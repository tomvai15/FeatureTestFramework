Feature: VerifyResponseBody_UsingRegexPlaceholders

To check if response fields has value, which matches specific pattern following placeholders can be used:
- AnyId matches only positive integers
- AnyNumber matches any integer, decimal values
- AnyString matches any text values
- AnyIsoDate matches Iso date format (yyyy-MM-ddThh:mm:ss.fffffffZ, yyyy-MM-dd hh:mm:ss.fffffffZ, yyyy-MM-dd hh:mm:ss.fffffff)
- AnyGuid matches guid value
- EmptyArray matches []

Background:
	Given I set sub Uri to "/api/v1"

Scenario: the response body should match. Verify that AnyGuid, AnyIsoDate and AnyId placeholders are replaced.
	Given I have an HTTP "POST" "/cars/1/tires" request with body
	"""
	{
		"tire": {
			"id": 1,
			"creationDate": "2011-10-05T14:48:00.0000000Z",
			"manufacturerCode": "327d7cdb-de0b-4221-be51-6f9dedf02b55",
			"carId": 2
		}
	}
	"""
	When I send the request
	Then the response status code should be 200
	And the response body should match
	"""
	{
		"tire": {
			"id": {{AnyId}},
			"creationDate": "{{AnyIsoDate}}",
			"manufacturerCode": "{{AnyGuid}}",
			"carId": {{AnyNumber}}
		}
	}
	"""

Scenario: the response body should match. Verify that multiple placeholders are replaced
	Given I have an HTTP "POST" "/cars" request with body
	"""
	{
		"car": {
			"id": 1,
			"name": "This is name"
		}
	}
	"""
	When I send the request
	Then the response status code should be 200
	And the response body should match
	"""
	{
		"car": {
			"id": 1,
			"name": "{{AnyString}} {{AnyString}} {{AnyString}}"
		}
	}
	"""

Scenario: the response body should match. Verify empty array
	Given I have an HTTP "POST" "/cars" request with body
	"""
	{
		"car": {
			"id": 1,
			"name": "This is name"
		}
	}
	"""
	When I send the request
	Then the response status code should be 200
	And the response body should match
	"""
	{
		"car": {
			"tires": {{EmptyArray}}
		}
	}
	"""