Feature: VerifyResponseBody_UsingExactFormatting

To check if object exists in the body without asserting specific values,
it is needed to use `the response body should match exact formatting` to avoid reformating json.
If reformating is applied, brackets are moved to the single line, because of this actual and expected responses won't match/

To validate this behaviour folllowing endpoints are used:
Endpoint POST /cars which returns 400, when it doesn't receive following body:
{
	"id": int,
	"name": string
}

Background:
	Given I set sub Uri to "/api/v1"

Scenario: the response body should match exact formatting. Does not reformat json when comparing actual and expected respone
	Given I have an HTTP "POST" "/cars" request with body
		"""
		{
		}
		"""
	When I send the request
	Then the response status code should be 400
	And the response body should match exact formatting
		"""
		{
			"title": "One or more validation errors occured.",
			"status": 400,
			"error": {
			}
		}
		"""
