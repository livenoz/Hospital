import { Injectable } from "@angular/core";

@Injectable()
export class Constants {

  static MODAL = class {
    public static readonly DISABLE_HEADER = "Vô hiệu hóa";
    public static readonly DISABLE_CONTENT = "Bệnh nhân đang chọn sẽ bị vô hiệu hóa, bấm <b>\"Xác nhận\"</b> để hoàn tất";
    public static readonly INFORMATION = "Thông báo";
    public static readonly UPDATE_SUCCESS = "Cập nhật thành công ! ";
    public static readonly UPDATE_FAIL = "Cập nhật thất bại !";
    public static readonly ADD_SUCCESS = "Thêm mới thành công ! ";
    public static readonly ADD_FAIL = "Thêm mới thất bại !";
  };

  public static readonly BASE_URL = "https://localhost:44306";
  public static readonly API_URL = "/api";
  public static readonly API_VERSION_1 = "/v1";

  static API_USERS = class {
    public static readonly login = "/users/login";
  };

  static API_PATIENTS = class {
    public static readonly patient = "/patient";
  };
}