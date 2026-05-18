Feature: CreateLicense

Scenario: Scenario 1
	Given I have an HTTP "POST" "License" request with body
	"""
	{
	  "featureLevel": "Basic",
	  "productType": "VideoEditor",
	  "userId": "A1SA5X"
	}
	"""
	And service "FeatureFlagService" returns 200 for "POST" "FeatureFlags" with body
	"""
	{
	  "featureFlag": "create_licenses_enabled",
	  "isEnabled": false
	}
	"""
	When I send the request
	Then the response status code should be 404
	And service "FeatureFlagService" was called with "POST" "FeatureFlags" and body
	"""
	{
	  "featureFlag": "create_licenses_enabled"
	}
	"""

Scenario: Scenario 2
	Given I have an HTTP "POST" "License" request with body
	"""
	{
	  "featureLevel": "Basic",
	  "productType": "VideoEditor",
	  "userId": "A1SA5X"
	}
	"""
	And service "FeatureFlagService" returns 200 for "POST" "FeatureFlags" with body
	"""
	{
	  "featureFlag": "create_licenses_enabled",
	  "isEnabled": true
	}
	"""
	And service "LicenseBackendService" returns 200 for "POST" "NewLicense"
	When I send the request
	Then the response status code should be 200
	And service "FeatureFlagService" was called with "POST" "FeatureFlags" and body
	"""
	{
	  "featureFlag": "create_licenses_enabled"
	}
	"""
	And service "LicenseBackendService" was called with "POST" "NewLicense" and body
	"""
	{
	  "featureLevel": "Basic",
	  "productType": "VideoEditor",
	  "userId": "A1SA5X"
	}
	"""
