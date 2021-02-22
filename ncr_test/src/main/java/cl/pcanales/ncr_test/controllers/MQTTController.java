package cl.pcanales.ncr_test.controllers;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import cl.pcanales.ncr_test.request.MQTTComandRequest;
import cl.pcanales.ncr_test.response.MQTTResponse;
import cl.pcanales.ncr_test.services.IMQTTService;

@RestController
@RequestMapping("/api")
@Validated
public class MQTTController {
	
	@Autowired
	private IMQTTService serviceMQTT;
	
	@PostMapping("/send")
	public ResponseEntity<MQTTResponse> sendComand(@RequestBody(required = true) 
	                                               MQTTComandRequest command) {
		
		// Validate the binding request body parameter
		if(command.getCommand() == null || command.getDestination() == null || command.getSender() == null) {
			
			//Create Bad Request Response
			MQTTResponse response = new MQTTResponse("Missing Parameters", null, 400);
			
			//Standard response sending to client 
			return new ResponseEntity<MQTTResponse>(response, HttpStatus.valueOf(response.getStatus()));
		}
		
		MQTTResponse response = serviceMQTT.sendCommand(command);
		
		//Standard response sending to client 
		return new ResponseEntity<MQTTResponse>(response, HttpStatus.valueOf(response.getStatus()));
	}

}
