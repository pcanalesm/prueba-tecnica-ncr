package cl.pcanales.ncr_test.services;

import cl.pcanales.ncr_test.request.MQTTComandRequest;
import cl.pcanales.ncr_test.response.MQTTResponse;

public interface IMQTTService {
	
	public MQTTResponse sendCommand(MQTTComandRequest mttqCommand);

}
