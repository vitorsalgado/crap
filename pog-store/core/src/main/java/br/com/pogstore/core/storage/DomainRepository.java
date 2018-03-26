/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.storage;

import java.util.UUID;

import br.com.pogstore.core.domain.AggregateRoot;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public interface DomainRepository {

	<T extends AggregateRoot> T get(UUID id);

	void store(AggregateRoot aggregateRoot);

}
