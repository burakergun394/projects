import { useEffect, useState } from "react";
import { Select, notification } from "antd";
import { Tenant } from "@/model/tenant";
import { useLocalization } from "@/localization/useLocalization";
import { useTenant } from "@/hooks/tenant";
import { executeActionWithNotifications } from "@/utils/notification";
import { AppstoreAddOutlined } from "@ant-design/icons";

interface TenantSelectProps {
  value?: string;
  onChange?: (value: string) => void;
}

const TenantSelect = ({ value, onChange }: TenantSelectProps) => {
  const [tenants, setTenants] = useState<Tenant[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const { getTenants } = useTenant();
  const { t } = useLocalization();
  const [api, contextHolder] = notification.useNotification();

  useEffect(() => {
    const fetchTenants = async () => {
      setLoading(true);

      const response = await executeActionWithNotifications<Tenant[]>(
        getTenants,
        t
      );

      if (response.isSuccess) {
        setTenants(response.data || []);
      }

      setLoading(false);
    };

    fetchTenants();
  }, []);

  return (
    <>
      {contextHolder}
      <Select
        prefix={<AppstoreAddOutlined />}
        value={value}
        onChange={onChange}
        placeholder={t("selectTenant")}
        options={tenants.map((tenant) => ({
          value: tenant.code,
          label: tenant.name,
        }))}
        loading={loading}
      />
    </>
  );
};

export default TenantSelect;
