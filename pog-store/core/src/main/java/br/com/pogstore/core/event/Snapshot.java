/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.event;

import java.util.UUID;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public abstract class Snapshot {

	public abstract UUID getId();

	public abstract void setId(UUID id);

	public abstract long getVersion();

	public abstract void setVersion(long version);
}
