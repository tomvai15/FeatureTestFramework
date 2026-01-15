Feature: CreateLicense

Scenario: Scenario 1
	Given I have an HTTP "POST" "PostLicense" request with body
	"""
	{
	  "featureLevel": "Basic",
	  "productType": "VideoEditor",
	  "userId": "A1SA5X"
	}
	"""
	And service "FeatureFlagService" api for "POST" "PostFeatureFlags" returns
	"""
	{
	  "featureFlag": "create_licenses_enabled",
	  "isEnabled": "true"
	}
	"""
	And service "LicenseBackendService" api for "POST" "PostNewLicense" returns
	"""
	{
	  "productType": "VideoEditor",
	  "userId": "A1SA5X"
	}
	"""
	When I send the request
	Then the response status code should be 200
	And service "FeatureFlagService" was called with "POST" "PostFeatureFlags"
	"""
	{
	  "featureFlag": "create_licenses_enabled"
	}
	"""
	And service "LicenseBackendService" was called with "POST" "PostNewLicense"
	"""
	{
	  "featureLevel": "Basic",
	  "productType": "VideoEditor",
	  "userId": "A1SA5X"
	}
	"""