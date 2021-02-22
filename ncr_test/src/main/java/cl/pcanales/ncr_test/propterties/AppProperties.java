package cl.pcanales.ncr_test.propterties;

import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

@Component
@ConfigurationProperties("app")
public class AppProperties {

	private String mqtt_uri;
	private String client;
	

	public String getMqtt_uri() {
		return mqtt_uri;
	}



	public void setMqtt_uri(String mqtt_uri) {
		this.mqtt_uri = mqtt_uri;
	}



	public String getClient() {
		return client;
	}



	public void setClient(String client) {
		this.client = client;
	}


	
	
}
