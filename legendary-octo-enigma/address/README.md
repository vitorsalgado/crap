# CRUD de Endereço

## Sobre
A idéia do projeto foi utilizar frameworks java "light-weigth". 
A api rest é baseada em Spark, injeção de dependências é gerenciado pelo Google Guice, já o acesso ao banco de dados utiliza uma solução em Hibernate e outra In Memory. O switch destas implementações pode ser feito na classe DependencyManager.

## Arquitetura
A arquitetura é simples, possui alguns aspectos de DDD e princípios SOLID. No geral, foi evitado controle de fluxos com exceptions. O recurso java.util.Optional é utilizado sempre que possível para evitar utilização de nulls para situações em que uma informação não é encontrada.
A camada de serviço, br.com.octoenigma.address.services, possui as principais funcionalidades do sistema. Os métodos sempre retornam um Response<T>, onde T deve ser preferencialmente um DTO (Data transfer object), garantindo que core da aplicação não seja exposto. 
Os controllers não possuem regras de negócio de complexas. Apenas validam e realizam o parse das informações que estão vindo e retornam a resposta para o consumidor.

## Insert
POST http://localhost:9090/api/cep HTTP/1.1

{
    "zipCode": "05416000",
    "type": "rua teste",
    "street": "rua lugar nenhum",
    "number": "200",
    "neighborhood": "pinheiros",
    "city": "sao paulo",
    "state": "sp"
}

## Update
PUT http://localhost:9090/api/cep HTTP/1.1

{
    "id": 1,
    "zipCode": "05416000",
    "type": "rua ATUALIZADA",
    "street": "rua lugar nenhum",
    "number": "200",
    "neighborhood": "pinheiros",
    "city": "sao paulo",
    "state": "sp"
}

## Select
GET http://localhost:9090/api/cep/1 
HTTP/1.1

## Delete
DELETE http://localhost:9090/api/cep/1 HTTP/1.1

## How To
* mvn clean package
* cd target
* java -jar address-1.0.0.jar