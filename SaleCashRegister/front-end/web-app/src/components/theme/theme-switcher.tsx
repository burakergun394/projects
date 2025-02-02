"use client";

import { Dropdown, Button, Space } from "antd";
import { BgColorsOutlined } from "@ant-design/icons";
import { useTheme } from "@/context/theme/ThemeContext";
import { useLocalization } from "@/localization/useLocalization";
import { ThemeMode } from "@/context/theme/types";

const themeItems = [
  {
    key: 'light',
    label: 'Light Mode',
    color: '#ffffff',
    textColor: '#000000',
  },
  {
    key: 'dark',
    label: 'Dark Mode',
    color: '#141414',
    textColor: '#ffffff',
  },
];

const ThemeSwitcher = () => {
  const { theme, setTheme } = useTheme();
  const { t } = useLocalization();

  const themeMenu = {
    items: themeItems.map(item => ({
      key: item.key,
      label: (
        <Space>
          <div style={{ 
            width: 16, 
            height: 16, 
            backgroundColor: item.color,
            borderRadius: 4,
            border: '1px solid #d9d9d9',
            color: item.textColor,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            fontSize: 12,
          }}>
            {item.key === 'dark' ? '🌙' : '☀️'}
          </div>
          {t(item.key === 'dark' ? 'darkMode' : 'lightMode')}
        </Space>
      ),
      onClick: () => setTheme({ mode: item.key as ThemeMode }),
    })),
  };

  return (
    <Dropdown menu={themeMenu} placement="bottomRight">
      <Button 
        icon={<BgColorsOutlined />}
        type="text"
        title={t("changeThemeColor")}
      />
    </Dropdown>
  );
};

export default ThemeSwitcher; 