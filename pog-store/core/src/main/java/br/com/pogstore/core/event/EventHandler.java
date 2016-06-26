/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.event;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 * @param <T>
 */
public interface EventHandler<T extends Event> {

	void handle(T event);

}
