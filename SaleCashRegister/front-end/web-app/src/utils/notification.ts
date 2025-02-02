import { BaseResponse } from "@/model/base";
import { notification } from "antd";

type NotificationType = "success" | "error" | "warning";

interface NotificationProps {
  type: NotificationType;
  message: string;
  description?: string;
}

export const showNotification = ({
  type,
  message,
  description,
}: NotificationProps) => {
  notification[type]({
    message,
    description,
    duration: 3,
    placement: "topRight",
  });
};

export const executeActionWithNotifications = async <T>(
    action: () => Promise<BaseResponse<T>>,
    t: (key: string) => string
  ): Promise<BaseResponse<T>> => {
    try {
      const response = await action();
  
      if (response.isSuccess) {
        showNotification({
          type: "success",
          message: t("success"),
          description: t(response.message) || t("operationCompletedSuccessfully"),
        });
      } else {
        showNotification({
          type: "error",
          message: t("error"),
          description: t(response.message) || t("anErrorOccurred"),
        });
      }
  
      return response;
    } catch (error) {
      const errorMessage = error instanceof Error ? error.message : t("anUnexpectedErrorOccurred");
  
      showNotification({
        type: "error",
        message: t("error"),
        description: t(errorMessage),
      });
  
      return {
        isSuccess: false,
        message: t(errorMessage),
        data: null as T,
      };
    }
  };
  