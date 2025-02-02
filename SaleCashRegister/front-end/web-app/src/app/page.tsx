"use client";

import { Typography } from "antd";
import { useLocalization } from "@/localization/useLocalization";

const { Title } = Typography;

export default function Home() {
  const { t } = useLocalization();
  
  return (
    <div>
      <Title level={2}>{t("welcome")}</Title>
    </div>
  );
}
