using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace auto_ponto
{
    class Program
    {
        const string CONFIG_PONTO_LOGIN_URL = "pontoLoginUrl";
        const string CONFIG_USERNAME = "networkUsername";
        const string CONFIG_PASSWORD = "networkPassword";
        const string CONFIG_ENABLE_JUSTIFICATION = "enableCustomJustification";

        enum Apontamento { ENTRADA = 1, INICIO_ALMOCO = 2, VOLTA_ALMOCO = 3, SAIDA = 4 }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(":: Apontamento de Horas Automático v1.0 ::"); Console.WriteLine("::"); Console.WriteLine("");

                Run(args);

                Console.WriteLine(""); Console.WriteLine(":: Apontamento realizado com sucesso! ::"); Console.WriteLine("::");

                Console.WriteLine(":: Fechando em 3 segundos..."); Thread.Sleep(1000);
                Console.WriteLine(":: 2..."); Thread.Sleep(1000);
                Console.WriteLine(":: 1..."); Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("Erro: ", ex.Message));
                Console.WriteLine("::");
                Console.WriteLine("::");
                Console.WriteLine(ex.ToString());

                Console.ReadLine();
            }
        }

        static void Run(string[] args)
        {
            var chromeDriverPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "driver");
            bool enableCustomJustification = false;

            string urlLogin = ConfigurationManager.AppSettings[CONFIG_PONTO_LOGIN_URL];
            if (string.IsNullOrEmpty(urlLogin))
                throw new InvalidOperationException(string.Format("Url de login do sistema de ponto não definida! Verifique a chave \"{0}\" no app.config.", CONFIG_PONTO_LOGIN_URL));

            string username = ConfigurationManager.AppSettings[CONFIG_USERNAME];
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException(string.Format("Nome de usuário não configurado! Verifique a chave \"{0}\" no app.config.", CONFIG_USERNAME));

            string password = ConfigurationManager.AppSettings[CONFIG_PASSWORD];
            if (string.IsNullOrEmpty(password))
                throw new InvalidOperationException(string.Format("Senha de usuário não configurada! Verifique a chave \"{0}\" no app.config.", CONFIG_PASSWORD));

            if (!bool.TryParse(ConfigurationManager.AppSettings[CONFIG_ENABLE_JUSTIFICATION], out enableCustomJustification))
                enableCustomJustification = false;

            string inputApontamento = string.Empty;

            if (args == null || !args.Any())
            {
                Console.WriteLine("Informe o apontamento:  1-Entrada. 2-Início almoço. 3-Volta Almoço. 4-Saída.");
                inputApontamento = Console.ReadLine();
            }
            else
            {
                inputApontamento = args[0];
            }

            Console.WriteLine("");

            int apontamento = 0;
            if (!int.TryParse(inputApontamento, out apontamento))
                throw new InvalidOperationException(string.Format("Apontamento não definido corretamente. Valor informado: \"{0}\"! Utilize:  1-Entrada.  2-Início almoço.  3-Volta Almoço.  4-Saída.", inputApontamento));

            using (var driver = new ChromeDriver(chromeDriverPath))
            {
                driver.Navigate().GoToUrl(urlLogin);

                var usernameField = driver.FindElementById("ctl00_m_g_1f2f91c5_726b_47c3_a64d_34117653e0e4_ctl00_signInControl_UserName");
                var passwordField = driver.FindElementById("ctl00_m_g_1f2f91c5_726b_47c3_a64d_34117653e0e4_ctl00_signInControl_Password");
                var buttonLogin = driver.FindElementById("ctl00_m_g_1f2f91c5_726b_47c3_a64d_34117653e0e4_ctl00_signInControl_LoginButton");

                usernameField.Clear();
                usernameField.SendKeys(username);

                passwordField.Clear();
                passwordField.SendKeys(password);

                buttonLogin.Click();

                IWait<IWebDriver> firstWait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
                firstWait.Until(x => driver.FindElementById("entrada") != null);

                string btnApontamentoId = string.Empty;

                switch ((Apontamento)apontamento)
                {
                    case Apontamento.ENTRADA:
                        btnApontamentoId = "entrada";
                        break;

                    case Apontamento.INICIO_ALMOCO:
                        btnApontamentoId = "inicioAlmoco";
                        break;

                    case Apontamento.VOLTA_ALMOCO:
                        btnApontamentoId = "voltaAlmoco";
                        break;

                    case Apontamento.SAIDA:
                        btnApontamentoId = "saida";
                        break;
                }

                var btnApontamento = driver.FindElementById(btnApontamentoId);
                btnApontamento.Click();

                IWait<IWebDriver> secondWait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
                secondWait.Until(x => driver.FindElementById("entrada") != null);

                if ((Apontamento)apontamento == Apontamento.SAIDA)
                {
                    try
                    {
                        driver.SwitchTo().DefaultContent();

                        var dialogIFrame = driver.FindElement(By.Id(""));
                        driver.SwitchTo().Frame(dialogIFrame);

                        var textAreaJustificativa = driver.FindElement(By.Id("ctl00_m_g_cac22e8d_9ad5_4163_b85d_7254c3524eac_ctl00_txtJustificativa"));
                        var btnConfirmarJustificativa = driver.FindElement(By.Id("ctl00_m_g_cac22e8d_9ad5_4163_b85d_7254c3524eac_ctl00_btnSalvar"));

                        string justificativa = ".";

                        if (enableCustomJustification)
                        {
                            Console.WriteLine("::");
                            Console.WriteLine(":: Informe a justificativa das horas extras: ");
                            justificativa = Console.ReadLine();

                            Console.WriteLine();
                        }

                        textAreaJustificativa.SendKeys(justificativa);
                        btnConfirmarJustificativa.Click();

                        IWait<IWebDriver> thirdWait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
                        thirdWait.Until(x => driver.FindElementById("entrada") != null);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(string.Concat("Erro: ", ex.Message));
                        Console.WriteLine("::");
                        Console.WriteLine("::");
                        Console.WriteLine(ex.ToString());
                        Console.WriteLine("");
                    }
                    finally
                    {
                        driver.SwitchTo().DefaultContent();
                    }
                }

                driver.Close();
            }
        }
    }
}
