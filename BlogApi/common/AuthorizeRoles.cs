using BlogApi.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace BlogApi.common
{
    public class AuthorizeMultipleRoles : Attribute, IAuthorizationFilter
    {
        string[] roles;
        IJwtAuthManager jwtAuthManager;
        bool requireAll;

        public AuthorizeMultipleRoles(IJwtAuthManager jwtAuthManager, string roles, bool requireAll = false)
        {
            this.roles = roles.Split(',');
            this.jwtAuthManager = jwtAuthManager;
            this.requireAll = requireAll;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {
                // get auth token from header of req and verify the roles/claims of user 
                var hasToken = filterContext.HttpContext.Request.Headers.ContainsKey("Authorization");

                string _token = hasToken ? filterContext.HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1] : null;

                if (string.IsNullOrEmpty(_token))
                {
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                    filterContext.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Invalid Token"
                        },
                    };
                    return;
                }
                else if (!ValidateToken(_token))
                {
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                    filterContext.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "not a vaild token"
                        },
                    };
                    return;
                }
                var hasRoles = requireAll ? HasRolesRequireAll(_token) : HasRoles(_token);
                if (hasRoles)
                {
                    return;
                }
                else if (!hasRoles)
                {
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                    filterContext.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Not Authorized"
                        },
                    };
                    return;
                }
            }

        }
        private bool ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var validations = jwtAuthManager.GetTokenValidationParameters();
                //fetch claims from auth token if throw exception means not a valid token
                handler.ValidateToken(token, validations, out var tokenSecure);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool HasRolesRequireAll(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var validations = jwtAuthManager.GetTokenValidationParameters();
                //fetch claims from auth token
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
                //Get role claim
                var roles = claims.FindFirst(claim => claim.Type == ClaimTypes.Role).Value.Split(',');
                var listRoles = this.roles.ToList();
                foreach (var role in roles)
                {
                    var hasRole = listRoles.Contains(role);
                    if (hasRole)
                        listRoles.Remove(role);
                }
                return listRoles.Count == 0;
            }
            catch
            {
                return false;
            }
        }

        private bool HasRoles(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var validations = jwtAuthManager.GetTokenValidationParameters();
                //fetch claims from auth token
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
                //Get role claim
                var roles = claims.FindFirst(claim => claim.Type == ClaimTypes.Role).Value.Split(',');
                var listRoles = this.roles.ToList();
                foreach (var role in roles)
                {
                    var hasRole = listRoles.Contains(role);
                    if (hasRole)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}