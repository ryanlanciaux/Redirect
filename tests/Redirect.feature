Feature: Redirection 
	In order to not upset the google
	As a blogger who almost never has the time to blog
	I want to redirect my old url to my new one 

Scenario: Redirect root request
	Given I have entered a request to http://www.frickinsweet.com/ryanlanciaux.com
	And the old url is frickinsweet.com/ryanlanciaux.com
	And my new url is ryanlanciaux.com
	When the request is made
	Then the response url is http://www.ryanlanciaux.com
	And the response has a 301 in the status

Scenario: Redirect to correct path on new url
	Given I have entered a request to http://www.frickinsweet.com/ryanlanciaux.com/page2
	And the old url is frickinsweet.com/ryanlanciaux.com
	And my new url is ryanlanciaux.com
	When the request is made
	Then the response url is http://www.ryanlanciaux.com/page2
	And the response has a 301 in the status
	
Scenario: Redirect returns false with garbage url
	Given I have entered a request to http://www.frickinsweet.com/ryanlanciazx.com
	And the old url is frickinsweet.com/ryanlanciaux.com
	And my new url is ryanlanciaux.com
	When the request is made
	Then 301 is not in the headers