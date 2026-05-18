Feature: SetRequestBody_UsingValueGeneratingPlaceholders


To add some generated value in the request body, foLLowing pLacehoLders can be used: 
	- AnyId generates positive integer 
	- NotExistingld returns 2^63 - 1 
	- AnyNegativeNumber generates negative integer 
	- AnyPositiveNumber generates positive integer 
	- AnyNumber generates positive integer some as AnyPositiveNumber
	- AnyDecimaL generates decimal, number with fractionaL part 
	- Today generates date with current time 
	- Past generates date which is in the past
	- Future generates date which is in the future 
	- Midnight generates last midnight date 00:00:00 
	- JustBeforeMidnight generates date before midnight 23:59:59 
	- JustAfterMidnight generates date after midnight 00:00:1 
	- NoDate generates date 0001-01T00:00:00.0000000 

To vatidate this behaviour following endpoints are used: 
Endpoint POST /verify-numbers and 
Endpoint POST /verify-dates 
which return 400 if request fields don't match required criteria. 

Background:
	Given I set sub Uri to "/api/v1/values"

Scenario: I have an HTTP request with body. Replaces number generating piaceholders
	Given I have an HTTP "POST" "/verify-numbers" request with body
		"""
		{ 
			"anyId": {{AnyId}}, 
			"notExistingId": {{NotExistingId}}, 
			"anyNegativeNumber": {{AnyNegativeNumber}}, 
			"anyPositiveNumber": {{AnyPositiveNumber}}, 
			"anyNumber": {{AnyNumber}}, 
			"anyDecimal": {{AnyDecimal}} 
		}
		"""
	When I send the request
	Then the response status code should be 200

Scenario: I have an HTTP request with body. Replaces date generating placeholders
	Given I have an HTTP "POST" "/verify-dates" request with body
		"""
		{
			"past": "{{Past}}", 
			"today": "{{Today}}", 
			"future": "{{Future}}", 
			"midnight": "{{Midnight}}", 
			"justBeforeMidnight": "{{JustBeforeMidnight}}", 
			"justAfterMidnight": "{{JustAfterMidnight}}", 
			"noDate": "{{NoDate}}" 
		} 
		"""
	When I send the request
	Then the response status code should be 200

