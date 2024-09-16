using ProjectSem3.DTOs;

namespace ProjectSem3.Services.PolicyService;

public interface PolicyService
{
    public bool create(PolicyDTO policyDTO);

    public bool update(PolicyDTO policyDTO);

    public List<PolicyDTO> getall();
}
