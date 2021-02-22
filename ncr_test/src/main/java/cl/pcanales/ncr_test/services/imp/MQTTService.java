package cl.pcanales.ncr_test.services.imp;


import org.eclipse.paho.client.mqttv3.IMqttClient;
import org.eclipse.paho.client.mqttv3.MqttClient;
import org.eclipse.paho.client.mqttv3.MqttConnectOptions;
import org.eclipse.paho.client.mqttv3.MqttException;
import org.eclipse.paho.client.mqttv3.MqttMessage;
import org.eclipse.paho.client.mqttv3.MqttPersistenceException;
import org.slf4j.LoggerFactory;
import org.slf4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import cl.pcanales.ncr_test.propterties.AppProperties;
import cl.pcanales.ncr_test.request.MQTTComandRequest;
import cl.pcanales.ncr_test.response.MQTTResponse;
import cl.pcanales.ncr_test.services.IMQTTService;


// Service 
@Service
public class MQTTService implements IMQTTService {

	//Initialize logger
	Logger logger = LoggerFactory.getLogger(MQTTService.class);
			
	@Autowired
	private AppProperties customAppProperties;

	@Override
	public MQTTResponse sendCommand(MQTTComandRequest mttqCommand) {


		try 
		{
			
			sendMessage(connect(), mttqCommand);
			
			return new MQTTResponse("", mttqCommand, 201);
		

		} catch (MqttException e) {
			
			logger.error(e.getMessage());
			
			return new MQTTResponse("Error in Mqtt Server", null, 404);
			
		} catch (JsonProcessingException e) {
			
			logger.error(e.getMessage());
			
			return new MQTTResponse("Error while process json message", null, 404);
		} catch(Exception e) {
			
			logger.error(e.getMessage());
			
			return new MQTTResponse("Internal Server error", null, 404);
		}
	}
	
	private IMqttClient connect() throws MqttException {
		
		//Initialize MQTT client with server uri and clientId
		IMqttClient client = new MqttClient(customAppProperties.getMqtt_uri(), customAppProperties.getClient());

		//Automatic reconnect
		MqttConnectOptions options = new MqttConnectOptions();
		options.setAutomaticReconnect(true);
		
		
		//Connect to server
		client.connect(options);
		
		return client;
	}
	
	private void sendMessage(IMqttClient client, MQTTComandRequest mttqCommand) throws MqttPersistenceException, MqttException, JsonProcessingException {
		
		// Prepare Object for Json serializer
		ObjectMapper objectMapper = new ObjectMapper();
	
		// Create message with json string from MQTTComandRequest object
		MqttMessage message = new MqttMessage(objectMapper
											  .writeValueAsString(mttqCommand)
											  .getBytes());
		
		
		//Publish message
		client.publish(mttqCommand.getDestination(), message);
		
		//disconnect client 
		client.disconnect();
	}

}
