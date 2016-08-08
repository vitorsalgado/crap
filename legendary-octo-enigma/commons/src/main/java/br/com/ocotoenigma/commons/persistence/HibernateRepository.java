package br.com.ocotoenigma.commons.persistence;

import org.hibernate.Session;

public abstract class HibernateRepository {
	protected Session getSession() {
		return HibernateSessionFactory.getSession();
	}
}
