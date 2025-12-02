Feature: Įtraukti papildomos informacijos

  Scenario: Scenario 1
    Given I have an HTTP "POST" "PostUserInformation" request
    When I send the request
    Then the response status code should be 200
    And the response body should match
    """
    {
      "surname": "Petraitis",
      "name": "Petras"
    }
    """
