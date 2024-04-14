Feature: ExternalApi

Scenario: external api returns response body.
	Given I have an HTTP "GET" "api/v1/external-api" request
	And external api returns response body
		"""
		{
			"url": "result"
		}
		"""
	When I send the request
	Then external api should be called
	And the response status code should be 200
	And the response body should match
		"""
		{
			"url": "result"
		}
		"""
