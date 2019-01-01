import { Injectable } from '@angular/core';

@Injectable()
export class Constants {

  public static MODAL = class {
    public static readonly DISABLE_HEADER = 'Vô hiệu hóa';
    public static readonly DISABLE_CONTENT = 'Bệnh nhân đang chọn sẽ bị vô hiệu hóa, bấm <b>"Xác nhận"</b> để hoàn tất';
    public static readonly INFORMATION = 'Thông báo';
    public static readonly UPDATE_SUCCESS = 'Cập nhật thành công ! ';
    public static readonly UPDATE_FAIL = 'Cập nhật thất bại !';
    public static readonly ADD_SUCCESS = 'Thêm mới thành công ! ';
    public static readonly ADD_FAIL = 'Thêm mới thất bại !';
  };

  public static readonly BASE_URL = 'http://10.211.55.4:8081';
  public static readonly API_URL = '/api';
  public static readonly API_VERSION_1 = '/v1';

  public static API_USERS = class {
    public static readonly login = '/users/login';
  };

  public static API_PATIENTS = class {
    public static readonly patient = '/patient';
  };

  public static API_ADDRESSES = class {
    public static readonly country = '/address/getCountries';
    public static readonly province = '/address/getProvinces';
    public static readonly district = '/address/getDistricts';
  };

  public static API_MEDICAL_RECORDS = class {
    public static readonly medicalRecord = '/medicalRecord';
  };
}
