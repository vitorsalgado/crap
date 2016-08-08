package br.com.ocotoenigma.commons.persistence;

import org.hibernate.HibernateException;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.cfg.Configuration;

public class HibernateSessionFactory {
	private static final SessionFactory sessionFactory;

	static {
		try {
			sessionFactory = new Configuration()
					.addPackage("br.com.octoenigma")
					.configure()
					.buildSessionFactory();
			
		} catch (Throwable ex) {
			throw new ExceptionInInitializerError(ex);
		}
	}

	public static Session getSession() throws HibernateException {
		return sessionFactory.getCurrentSession();
	}
}