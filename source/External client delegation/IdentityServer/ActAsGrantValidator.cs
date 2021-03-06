﻿using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Validation;

namespace IdentityServer
{
  internal class ActAsGrantValidator : ICustomGrantValidator
  {
    private TokenValidator _validator;

    public ActAsGrantValidator(TokenValidator validator)
    {
      _validator = validator;
    }

    public string GrantType
    {
      get { return "act-as"; }
    }

    Task<CustomGrantValidationResult> ICustomGrantValidator.ValidateAsync(ValidatedTokenRequest request)
    {
      CustomGrantValidationResult grantResult = null;

      var param = request.Raw.Get("token");
      if (string.IsNullOrWhiteSpace(param))
      {
        grantResult = new CustomGrantValidationResult(Constants.TokenErrors.InvalidRequest);
      }

      var result = _validator.ValidateAccessTokenAsync(param).Result;
      if (result.IsError)
      {
        grantResult = new CustomGrantValidationResult(result.Error);
      }

      var subjectClaim = result.Claims.FirstOrDefault(x => x.Type == "sub");
      if (subjectClaim == null)
      {
        grantResult = new CustomGrantValidationResult(Constants.TokenErrors.InvalidRequest);
      }

      if (grantResult == null)
      {
        grantResult = new CustomGrantValidationResult(subjectClaim.Value, "access_token");
      }

      return Task.FromResult(grantResult);
    }
  }
}