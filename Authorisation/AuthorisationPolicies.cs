namespace CustomerManagementService.Authorisation
{
    public record AuthorisationPolicies(List<PolicyRequirement> Policies)
    {
        public static readonly AuthorisationPolicies Default = new AuthorisationPolicies(
            new List<PolicyRequirement>
            {
                new("ReadCustomer", new[] { "read:customer" }),
                new("CreateCustomer", new[] { "create:customer" }),
                new("UpdateCustomer", new[] { "update:customer" }),
                new("DeleteCustomer", new[] { "requestDelete:customer" }),
            });
    }

    public record PolicyRequirement(string PolicyName, string[] Permissions);
}