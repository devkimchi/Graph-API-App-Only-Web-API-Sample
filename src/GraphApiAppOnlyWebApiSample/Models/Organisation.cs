using System.Collections.Generic;

namespace GraphApiAppOnlyWebApiSample.Models
{
    /// <summary>
    /// This represents the model entity for organisation.
    /// </summary>
    public class Organisation
    {
        /// <summary>
        /// Gets or sets the list of <see cref="AssignedPlan"/> instances.
        /// </summary>
        public List<AssignedPlan> AssignedPlans { get; set; }

        /// <summary>
        /// Gets or sets the list of business phones.
        /// </summary>
        public List<string> BusinessPhones { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the country letter code.
        /// </summary>
        public string CountryLetterCode { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// This represents the model entity for assigned plan.
    /// </summary>
    public class AssignedPlan
    {
        /// <summary>
        /// Gets or sets the assigned date time.
        /// </summary>
        public string AssignedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the capability status.
        /// </summary>
        public string CapabilityStatus { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the service plan Id.
        /// </summary>
        public string ServicePlanId { get; set; }
    }
}