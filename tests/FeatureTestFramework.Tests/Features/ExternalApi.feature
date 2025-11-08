Feature: External api

Scenario: I set sub Uri. Appends sub uri and adjusts slashes.
	Given I have an HTTP "GET" "/api/v1/external-api" request
	And service "Postman" api for "GET" "/get" returns
	"""
	{
		"url": "Same url"
	}
	"""
	When I send the request
	Then the response status code should be 200
	And the response body should match
	"""
	{
		"url": "Same url"
	}
	"""
	And service "Postman" was called with "GET" "/get"
	