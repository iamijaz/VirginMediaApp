using Microsoft.Extensions.Options;
using VirginMediaApp.Scenarios.Core.Config;

namespace VirginMediaApp.Scenarios.Web.IOC;

public class CacheServicesConfigurationValidation : IValidateOptions<CacheServicesConfig>
{
    public ValidateOptionsResult Validate(string configKey, CacheServicesConfig options)
    {
        switch (configKey)
        {
            case CacheServicesConfig.CacheServices:
            {
                var result = ValidateScenarioApiConfig(options);
                if (result.Failed) return result;
                break;
            }
            default:
                return ValidateOptionsResult.Skip;
        }

        return ValidateOptionsResult.Success;
    }

    private static ValidateOptionsResult ValidateScenarioApiConfig(CacheServicesConfig options)
    {
        return options.MinsToCache < 10
            ? ValidateOptionsResult.Fail("The Scenario API cache must be at least 10 minutes.")
            : ValidateOptionsResult.Success;
    }
}