"use client";

import { Button, Dropdown, Space } from "antd";
import { useLocalization } from "@/localization/useLocalization";
import { Language } from "@/localization/types";
import { TranslationOutlined } from "@ant-design/icons";

interface LanguageOption {
  key: Language;
  label: string;
}

const LanguageSwitcher = () => {
  const { t, currentLanguage, changeLanguage } = useLocalization();

  const languageItems: LanguageOption[] = [
    { key: "tr", label: t("turkish") },
    { key: "en", label: t("english") },
  ];

  const languageMenu = {
    items: languageItems.map((item: LanguageOption) => ({
      key: item.key,
      label: item.label,
      onClick: () => changeLanguage(item.key),
    })),
  };

  return (
    <Space>
      <Dropdown menu={languageMenu} placement="bottomRight">
        <Button type="text" icon={<TranslationOutlined />}>
          {currentLanguage === "tr" ? "🇹🇷" : "🇺🇸"} {t(currentLanguage)}
        </Button>
      </Dropdown>
    </Space>
  );
};

export default LanguageSwitcher;
