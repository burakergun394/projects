import { Tenant } from "@/model/tenant";
import { BaseResponse } from "@/model/base";
import { baseFunction } from "@/utils/function";

export const useTenant = () => {
  const getTenants = async (): Promise<BaseResponse<Tenant[]>> => {
    return baseFunction(async () => {
      const tenants: Tenant[] = [
        { code: "TenantA", name: "TenantA" },
        { code: "TenantB", name: "TenantB" },
      ];

      const message =
        tenants.length > 0
          ? "Tenants retrieved successfully"
          : "No tenants found";

      return {
        data: tenants,
        message,
        isSuccess: tenants.length > 0,
      };
    });
  };

  return { getTenants };
};
