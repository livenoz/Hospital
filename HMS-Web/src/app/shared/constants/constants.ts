import { Injectable } from '@angular/core';

@Injectable()
export class Constants {

  public static MODAL = class {
    public static readonly DISABLE_HEADER = 'Vô hiệu hóa';
    public static readonly ENABLE_HEADER = 'Kích hoạt';
    public static readonly DISABLE_PATIENT_CONTENT = 'Bệnh nhân đang chọn sẽ bị vô hiệu hóa, bấm <b>"Xác nhận"</b> để hoàn tất';
    public static readonly ENABLE_PATIENT_CONTENT = 'Bệnh nhân đang chọn sẽ được kích hoạt lại, bấm <b>"Xác nhận"</b> để hoàn tất';
    public static readonly DISABLE_MEDICALRECORD_CONTENT = 'Bệnh án đang chọn sẽ bị vô hiệu hóa, bấm <b>"Xác nhận"</b> để hoàn tất';
    public static readonly ENABLE_MEDICALRECORD_CONTENT = 'Bệnh án đang chọn sẽ được kích hoạt lại, bấm <b>"Xác nhận"</b> để hoàn tất';
    public static readonly INFORMATION = 'Thông báo';
    public static readonly UPDATE_SUCCESS = 'Cập nhật thành công ! ';
    public static readonly UPDATE_FAIL = 'Cập nhật thất bại !';
    public static readonly ADD_SUCCESS = 'Thêm mới thành công ! ';
    public static readonly ADD_FAIL = 'Thêm mới thất bại !';
  };

  public static readonly BASE_URL = 'http://10.211.55.4:8081';
  // public static readonly BASE_URL = 'https://10.211.55.4:5001';
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

  public static API_EMPLOYEE = class {
    public static readonly doctor = '/employee/doctors';
    public static readonly nurse = '/employee/nurses';
  };

  public static API_MEDICAL_RECORDS = class {
    public static readonly medicalRecord = '/medicalRecord';
  };

  public static API_TREATMENT = class {
    public static readonly treatment = '/treatment';
    public static readonly disease = '/disease';
  };

  public static API_TREATMENT_DETAIL = class {
    public static readonly treatmentDetail = '/treatmentDetail';
    public static readonly disease = '/disease';
  };

  public static API_PRESCRIPTIONS = class {
    public static readonly prescription = '/prescription';
    public static readonly drug = '/prescription/drugs';
    public static readonly unit = '/prescription/units';
  };
}
