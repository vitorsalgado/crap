/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.event;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public interface Originator {

	Snapshot getSnapshot();

	void setSnapshot(Snapshot snapshot);

}
