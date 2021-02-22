package cl.pcanales.ncr_test.request;

import java.io.Serializable;




public class MQTTComandRequest implements Serializable {

    private static final long serialVersionUID = 1L;
	

	private String sender;
	
	private String destination;
	

	private String command;
	
	public MQTTComandRequest()
	{
		
	}
		
	
	public MQTTComandRequest(String sender, String destination, String command) {
		super();
		this.sender = sender;
		this.destination = destination;
		this.command = command;
	}


	public String getSender() {
		return sender;
	}
	public void setSender(String sender) {
		this.sender = sender;
	}
	public String getDestination() {
		return destination;
	}
	public void setDestination(String destination) {
		this.destination = destination;
	}
	public String getCommand() {
		return command;
	}
	public void setCommand(String command) {
		this.command = command;
	}
	
	

}
