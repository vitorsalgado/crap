package br.com.octoenigma.address.infrastructure.repositories;

import java.util.Optional;

import org.hibernate.Session;
import org.hibernate.Transaction;

import br.com.ocotoenigma.commons.persistence.HibernateRepository;
import br.com.octoenigma.address.domain.Address;
import br.com.octoenigma.address.domain.AddressRepository;

public class HibernateAddressRepository extends HibernateRepository implements AddressRepository {

	@SuppressWarnings("unchecked")
	@Override
	public Optional<Address> findById(final long id) {
		Session session = getSession();
		Transaction tx = session.beginTransaction();
		
		Optional<Address> address = session
				.createQuery("from Address where id = :id")
				.setParameter("id", id)
				.uniqueResultOptional();

		tx.commit();
		session.close();

		return address;
	}

	@SuppressWarnings("unchecked")
	@Override
	public Optional<Address> findByZipCode(final String zipCode) {
		Session session = getSession();
		Transaction tx = session.beginTransaction();

		Optional<Address> address = session
				.createQuery("from Address where zipCode = :zipCode")
				.setParameter("zipCode", zipCode)
				.uniqueResultOptional();

		tx.commit();
		session.close();

		return address;
	}

	@Override
	public void save(Address address) {
		Session session = getSession();
		Transaction tx = session.beginTransaction();

		session.saveOrUpdate(address);

		tx.commit();
		session.close();
	}

	@Override
	public void delete(Address address) {
		Session session = getSession();
		Transaction tx = session.beginTransaction();

		session.delete(address);

		tx.commit();
		session.close();
	}

}
