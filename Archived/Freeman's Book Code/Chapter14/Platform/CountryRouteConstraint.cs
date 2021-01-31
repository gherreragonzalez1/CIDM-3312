using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Platform {
    public class CountryRouteConstraint: IRouteConstraint {
        private static string[] countries = { "uk", "france", "monaco" };

        public bool Match(HttpContext httpContext, IRouter router, string routeKey,
                RouteValueDictionary valuePairs, RouteDirection routeDirection) {
            string segmentValue = valuePairs[routeKey] as string ?? "";
            return Array.IndexOf(countries, segmentValue.ToLower()) > -1;
        }
    }
}