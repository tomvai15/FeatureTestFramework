# FeatureTestFramework


## About
The purpose of the library is to ensure that the service API functions correctly and returns expected results every time. This helps automate the testing process and reduces the risk of human error. This library is an excellent tool for creating reliable and repeatable tests, which are essential for ensuring the quality of the service API.

## Key Features
Provides Specflow steps for sending HTTP request and asserting response body, headers and status code. Request, response body support placeholders.

## Setup
Add ir update specflow.json file.
```json
{
  "stepAssemblies": [
    {
      "assembly": "FeatureTestsFramework" //add this line
    }
  ]
}
```
Create `TestStartup.cs` class with provided code.

```cs
using FeatureTestFramework.Tests.Bootstrapping;
using FeatureTestsFramework;
using FeatureTestsFramework.Bootstrapping;
using TechTalk.SpecFlow;

namespace FeatureTestFramework.Tests
{
    [Binding]
    public class TestStartup
    {
        [BeforeTestRun(Order = TestRunOrder.InjectServices)]
        public static void RegisterServices()
        {
            ConfigurationAccessor.AddUserSecrets<TestStartup>();
            var services = ServiceAccessor.ServiceCollection;
            services.AddCommonServices(ConfigurationAccessor.Configuration);
        }
    }
}

```

## How to Use

Steps provided by framework work like any other step. Usual scenario should have step to configure request and to assert response. 

Example scenario for sending request and checking response:
```feature
Scenario: the response body should match. Verifies that json response body is asserted
	Given I have an HTTP "GET" "/api/v1/cars/1" request
	When I send the request
	Then the response status code should be 200
	And the response body should match
		"""
		{
			"id": 1,
			"name": "Car"
		}
		"""
```

## Changelog
 ### 1.0.0 Initial release
 ### 1.0.1 Method name fixes

## License
This project is licensed under the MIT License.