package br.com.octoenigma.zipcode.repositories;

import java.util.Optional;

import org.hibernate.Session;
import org.hibernate.Transaction;

import br.com.ocotoenigma.commons.persistence.HibernateRepository;
import br.com.octoenigma.zipcode.models.ZipCode;
import br.com.octoenigma.zipcode.models.ZipCodeRepository;

public class HibernateZipCodeRepository extends HibernateRepository implements ZipCodeRepository {

	@SuppressWarnings("unchecked")
	@Override
	public Optional<ZipCode> findByCode(String code) {
		Session session = getSession();
		Transaction tx = session.beginTransaction();

		Optional<ZipCode> zipCode = session
				.createQuery("from ZipCode where code = :code").setParameter("code", code)
				.uniqueResultOptional();

		tx.commit();
		session.close();

		return zipCode;
	}

}
