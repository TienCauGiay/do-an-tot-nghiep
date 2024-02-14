<template>
  <div id="detail-info-employee" class="position-display-center" ref="FormDetail">
    <div class="form-detail-toolbar">
      <div class="question-icon icon-tb" :tabindex="23" :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.HELP"></div>
      <div
        @click="onCloseFormDetail"
        class="close-icon icon-tb"
        id="employee-exit"
        :tabindex="24"
        @keydown.tab.prevent="resetTab($event.target.value)"
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.EXIST"
      ></div>
    </div>
    <div class="form-detail-main">
      <div class="employee-title">
        <p>
          <b>{{ this.titleFormMode }}</b>
        </p>
        <div class="entity-check">
          <input
            type="checkbox"
            :checked="employee.IsCustomer === this.$_MSEnum.CUSTOMER"
            @change="handleCustomerCheckboxChange"
            :tabindex="18"
          />
          <span>{{ this.$_MSResource[this.$_LANG_CODE].FORM.IS_CUSTOMER }}</span>
        </div>
        <div class="entity-check">
          <input
            type="checkbox"
            :checked="employee.IsProvider === this.$_MSEnum.PROVIDER"
            @change="handleProviderCheckboxChange"
            :tabindex="19"
          />
          <span>{{ this.$_MSResource[this.$_LANG_CODE].FORM.IS_PROVIDER }}</span>
        </div>
      </div>
      <div class="form-detail-content">
        <div class="half-content">
          <div class="col-md-n">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.CODE }}
              <span class="s-require">*</span>
            </label>
            <div class="container-input">
              <ms-input
                ref="EmployeeCode"
                v-model="employee.EmployeeCode"
                :class="{ 'border-red': isBorderRed.EmployeeCode }"
                :tabindex="1"
                @input="setIsBorderRed('EmployeeCode')"
                @mouseenter="isHovering.EmployeeCode = true"
                @mouseleave="isHovering.EmployeeCode = false"
              ></ms-input>
              <div
                class="ms-tooltip"
                v-if="
                  isHovering.EmployeeCode &&
                  (isBorderRed.EmployeeCode || !employee.EmployeeCode) &&
                  errors['EmployeeCode']
                "
              >
                {{ errors["EmployeeCode"] }}
              </div>
            </div>
          </div>
          <div class="col-md-tb">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.NAME }}
              <span class="s-require">*</span>
            </label>
            <div class="container-input">
              <ms-input
                ref="FullName"
                v-model="employee.FullName"
                :class="{ 'border-red': isBorderRed.FullName }"
                :tabindex="2"
                @input="setIsBorderRed('FullName')"
                @mouseenter="isHovering.FullName = true"
                @mouseleave="isHovering.FullName = false"
              ></ms-input>
              <div
                class="ms-tooltip"
                v-if="isHovering.FullName && (isBorderRed.FullName || !employee.FullName) && errors['FullName']"
              >
                {{ errors["FullName"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-n">
            <label>{{ this.$_MSResource[this.$_LANG_CODE].FORM.DOB }}</label>
            <div class="container-input">
              <ms-input
                type="date"
                v-model="employee.DateOfBirth"
                :value="formattedDate"
                :tabindex="5"
                :class="{
                  'border-red': isBorderRed.DateOfBirth,
                }"
                ref="DateOfBirth"
                @input="setIsBorderRed('DateOfBirth')"
                @mouseenter="isHovering.DateOfBirth = true"
                @mouseleave="isHovering.DateOfBirth = false"
              ></ms-input>
              <!-- <VueDatePicker 
                :class="{ 'border-red': isBorderRed.DateOfBirth }" 
                v-model="employee.DateOfBirth" 
                :format="'dd/MM/yyyy'" 
                auto-apply 
                :clearable="false"
                :placeholder="'dd/mm/yyyy'"
                :tabindex="5"
                ref="DateOfBirth" 
                @mouseenter="isHovering.DateOfBirth = true"
                @mouseleave="isHovering.DateOfBirth = false"
              ></VueDatePicker>
              <div class="ms-tooltip" v-if="!isShowDialogDataNotNull ? isHovering.DateOfBirth && isBorderRed.DateOfBirth : false">
                {{ errors["DateOfBirth"] }}
              </div> -->
              <div class="ms-tooltip" v-if="isHovering.DateOfBirth && isBorderRed.DateOfBirth">
                {{ errors["DateOfBirth"] }}
              </div>
            </div>
          </div>
          <div class="col-md-tb">
            <label>{{ this.$_MSResource[this.$_LANG_CODE].FORM.GENDER }}</label>
            <div class="e-gender">
              <input
                v-model="employee.Gender"
                :value="this.$_MSEnum.GENDER.Male"
                type="radio"
                name="gender"
                :tabindex="6"
              />
              <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.GENDER.Male }}</span>
              <input
                v-model="employee.Gender"
                :value="this.$_MSEnum.GENDER.Female"
                type="radio"
                name="gender"
                :tabindex="7"
              />
              <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.GENDER.Female }}</span>
              <input
                v-model="employee.Gender"
                :value="this.$_MSEnum.GENDER.Other"
                type="radio"
                name="gender"
                :tabindex="8"
              />
              <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.GENDER.Other }}</span>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemDepartment">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.DEPARTMENT }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              :isBorderRedCBB="isBorderRed"
              :entityCBB="employee"
              :errorsCBB="errors"
              :tabindexCBB="3"
              :listEntitySearchCBB="listDepartmentSearch"
              :propName="'DepartmentName'"
              :propId="'DepartmentId'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PLACEHOLDER_DEPARTMENT"
            ></ms-combobox>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-tb">
            <label :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.IDENTITY_NUMBER">
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.IDENTITY_NUMBER }}
            </label>
            <div class="container-input">
              <ms-input
                v-model="employee.IdentityNumber"
                :tabindex="9"
                ref="IdentityNumber"
                :class="{ 'border-red': isBorderRed.IdentityNumber }"
                @input="setIsBorderRed('IdentityNumber')"
                @mouseenter="isHovering.IdentityNumber = true"
                @mouseleave="isHovering.IdentityNumber = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.IdentityNumber && isBorderRed.IdentityNumber">
                {{ errors["IdentityNumber"] }}
              </div>
            </div>
          </div>
          <div class="col-md-n">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.IDENTITY_DATE }}
            </label>
            <div class="container-input">
              <ms-input
                type="date"
                v-model="employee.IdentityDate"
                :value="formattedDateIdentity"
                :tabindex="9"
                :class="{ 'border-red': isBorderRed.IdentityDate }"
                ref="IdentityDate"
                @input="setIsBorderRed('IdentityDate')"
                @mouseenter="isHovering.IdentityDate = true"
                @mouseleave="isHovering.IdentityDate = false"
              ></ms-input>
              <!-- <VueDatePicker 
                :class="{ 'border-red': isBorderRed.IdentityDate }" 
                v-model="employee.IdentityDate" 
                :format="'dd/MM/yyyy'" 
                auto-apply 
                :clearable="false"
                :placeholder="'dd/mm/yyyy'"
                :tabindex="9"
                ref="IdentityDate" 
                @mouseenter="isHovering.IdentityDate = true"
                @mouseleave="isHovering.IdentityDate = false"
              ></VueDatePicker>
              <div class="ms-tooltip" v-if="!isShowDialogDataNotNull ? isHovering.IdentityDate && isBorderRed.IdentityDate : false">
                {{ errors["IdentityDate"] }}
              </div> -->
              <div class="ms-tooltip" v-if="isHovering.IdentityDate && isBorderRed.IdentityDate">
                {{ errors["IdentityDate"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.POSITION }}
            </label>
            <div class="container-input">
              <ms-input
                v-model="employee.PositionName"
                :tabindex="4"
                ref="PositionName"
                :class="{ 'border-red': isBorderRed.PositionName }"
                @input="setIsBorderRed('PositionName')"
                @mouseenter="isHovering.PositionName = true"
                @mouseleave="isHovering.PositionName = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.PositionName && isBorderRed.PositionName">
                {{ errors["PositionName"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.IDENTITY_ADDRESS }}
            </label>
            <div class="container-input">
              <ms-input
                v-model="employee.IdentityPlace"
                :tabindex="10"
                ref="IdentityPlace"
                :class="{ 'border-red': isBorderRed.IdentityPlace }"
                @input="setIsBorderRed('IdentityPlace')"
                @mouseenter="isHovering.IdentityPlace = true"
                @mouseleave="isHovering.IdentityPlace = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.IdentityPlace && isBorderRed.IdentityPlace">
                {{ errors["IdentityPlace"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="full-content" id="content-null"></div>
        <div class="full-content">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.ADDRESS }}</label>
          <div class="container-input">
            <ms-input
              v-model="employee.Address"
              :tabindex="11"
              ref="Address"
              :class="{ 'border-red': isBorderRed.Address }"
              @input="setIsBorderRed('Address')"
              @mouseenter="isHovering.Address = true"
              @mouseleave="isHovering.Address = false"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.Address && isBorderRed.Address">
              {{ errors["Address"] }}
            </div>
          </div>
        </div>
        <div class="full-content">
          <div class="full-content-quarter">
            <div class="col-md-quater">
              <label :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.PHONE_NUMBER">
                {{ this.$_MSResource[this.$_LANG_CODE].FORM.PHONE_NUMBER }}
              </label>
              <div class="container-input">
                <ms-input
                  v-model="employee.PhoneNumber"
                  :tabindex="12"
                  ref="PhoneNumber"
                  :class="{ 'border-red': isBorderRed.PhoneNumber }"
                  @input="setIsBorderRed('PhoneNumber')"
                  @mouseenter="isHovering.PhoneNumber = true"
                  @mouseleave="isHovering.PhoneNumber = false"
                ></ms-input>
                <div class="ms-tooltip" v-if="isHovering.PhoneNumber && isBorderRed.PhoneNumber">
                  {{ errors["PhoneNumber"] }}
                </div>
              </div>
            </div>
            <div class="col-md-quater">
              <label :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.PHONE_LANDLINE">
                {{ this.$_MSResource[this.$_LANG_CODE].FORM.PHONE_LANDLINE }}
              </label>
              <div class="container-input">
                <ms-input
                  v-model="employee.PhoneLandline"
                  :tabindex="13"
                  ref="PhoneLandline"
                  :class="{ 'border-red': isBorderRed.PhoneLandline }"
                  @input="setIsBorderRed('PhoneLandline')"
                  @mouseenter="isHovering.PhoneLandline = true"
                  @mouseleave="isHovering.PhoneLandline = false"
                ></ms-input>
                <div class="ms-tooltip" v-if="isHovering.PhoneLandline && isBorderRed.PhoneLandline">
                  {{ errors["PhoneLandline"] }}
                </div>
              </div>
            </div>
            <div class="col-md-quater">
              <label>
                {{ this.$_MSResource[this.$_LANG_CODE].FORM.EMAIL }}
              </label>
              <div class="container-input">
                <ms-input
                  v-model="employee.Email"
                  :tabindex="14"
                  :class="{ 'border-red': isBorderRed.Email }"
                  ref="Email"
                  @input="setIsBorderRed('Email')"
                  @mouseenter="isHovering.Email = true"
                  @mouseleave="isHovering.Email = false"
                ></ms-input>
                <div class="ms-tooltip" v-if="isHovering.Email && isBorderRed.Email">
                  {{ errors["Email"] }}
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="full-content">
          <div class="full-content-quarter">
            <div class="col-md-quater">
              <label>{{ this.$_MSResource[this.$_LANG_CODE].FORM.BANK_ACCOUNT }}</label>
              <div class="container-input">
                <ms-input
                  v-model="employee.BankAccount"
                  :tabindex="15"
                  ref="BankAccount"
                  :class="{ 'border-red': isBorderRed.BankAccount }"
                  @input="setIsBorderRed('BankAccount')"
                  @mouseenter="isHovering.BankAccount = true"
                  @mouseleave="isHovering.BankAccount = false"
                ></ms-input>
                <div class="ms-tooltip" v-if="isHovering.BankAccount && isBorderRed.BankAccount">
                  {{ errors["BankAccount"] }}
                </div>
              </div>
            </div>
            <div class="col-md-quater">
              <label>{{ this.$_MSResource[this.$_LANG_CODE].FORM.BANK_NAME }}</label>
              <div class="container-input">
                <ms-input
                  v-model="employee.BankName"
                  :tabindex="16"
                  ref="BankName"
                  :class="{ 'border-red': isBorderRed.BankName }"
                  @input="setIsBorderRed('BankName')"
                  @mouseenter="isHovering.BankName = true"
                  @mouseleave="isHovering.BankName = false"
                ></ms-input>
                <div class="ms-tooltip" v-if="isHovering.BankName && isBorderRed.BankName">
                  {{ errors["BankName"] }}
                </div>
              </div>
            </div>
            <div class="col-md-quater">
              <label>{{ this.$_MSResource[this.$_LANG_CODE].FORM.BANK_BRANCH }}</label>
              <div class="container-input">
                <ms-input
                  v-model="employee.BankBranch"
                  :tabindex="17"
                  ref="BankBranch"
                  :class="{ 'border-red': isBorderRed.BankBranch }"
                  @input="setIsBorderRed('BankBranch')"
                  @mouseenter="isHovering.BankBranch = true"
                  @mouseleave="isHovering.BankBranch = false"
                ></ms-input>
                <div class="ms-tooltip" v-if="isHovering.BankBranch && isBorderRed.BankBranch">
                  {{ errors["BankBranch"] }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="form-detail-action">
        <div class="action-left">
          <ms-button-extra
            :textButtonExtra="this.$_MSResource[this.$_LANG_CODE].BUTTON.CANCEL"
            @click="btnCancel"
            :tabindex="22"
          ></ms-button-extra>
        </div>
        <div class="action-right">
          <ms-button-extra
            :textButtonExtra="this.$_MSResource[this.$_LANG_CODE].BUTTON.SAVE"
            @click="btnSave"
            :tabindex="20"
            :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SAVE"
          ></ms-button-extra>
          <ms-button-default
            :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.SAVE_AND_ADD"
            @click="btnSaveAndAdd"
            :tabindex="21"
            :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SAVE_AND_ADD"
          ></ms-button-default>
        </div>
      </div>
    </div>
    <!-- dialog employee input data not blank -->
    <ms-dialog-data-not-null
      v-if="isShowDialogDataNotNull"
      :valueNotNull="dataNotNull"
      :title="this.$_MSResource[this.$_LANG_CODE].DIALOG.TITLE.DATA_INVALID"
    ></ms-dialog-data-not-null>
    <!-- dialog employee id Exist -->
    <ms-dialog-data-exist
      v-if="isShowDialogCodeExist"
      :textProp="this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_PRE"
      :textEntityCodeExist="contentEmployeeCodeExist"
    ></ms-dialog-data-exist>
    <!-- dialog employee save and close -->
    <ms-dialog-data-change v-if="isShowDialogDataChange"></ms-dialog-data-change>
  </div>
</template>

<script>
import employeeService from "@/services/employee.js";
import departmentService from "@/services/department.js";
import helperCommon from "@/scripts/helper.js";
// import VueDatePicker from '@vuepic/vue-datepicker';
// import '@vuepic/vue-datepicker/dist/main.css';

export default {
  name: "EmployeeDetail",

  props: ["employeeSelected", "statusFormMode"],

  // components: { VueDatePicker },

  created() {
    this.loadData();

    this.$_MSEmitter.on("cancelDialogDataChange", () => {
      this.onCancelDialogDataChange();
    });
    this.$_MSEmitter.on("noDialogDataChange", () => {
      this.onNoDialogDataChange();
    });
    this.$_MSEmitter.on("yesDialogDataChange", async () => {
      await this.onYesDialogDataChange();
    });

    this.$_MSEmitter.on("closeDialogCodeExist", () => {
      this.btnCloseDialogCodeExist();
    });

    this.$_MSEmitter.on("closeDialogDataError", () => {
      this.onCloseDialogSaveAndAdd();
    });

    this.$_MSEmitter.on("onSelectedEntityCBB", (data) => {
      this.onSelectedDepartment(data);
    });
    this.$_MSEmitter.on("onSearchChangeCBB", (newValue) => {
      this.onSearchChange(newValue);
    });
    this.$_MSEmitter.on("onKeyDownEntityCBB", (index) => {
      this.employee.DepartmentName = this.listDepartmentSearch[index].DepartmentName;
      this.employee.DepartmentId = this.listDepartmentSearch[index].DepartmentId;
      this.isBorderRed.DepartmentName = false;
    });
  },

  mounted() {
    // focus vào ô đầu tiên khi mở form chi tiết
    this.focusCode();
    // Đăng kí các sự kiện
    this.$refs.FormDetail.addEventListener("keydown", this.handleKeyDown);
  },

  data() {
    return {
      // Khai báo mảng lưu các thuộc tính cần validate theo thứ tự, phục vụ cho việc focus, hiển thị lỗi theo thứ tự
      employeeProperty: [
        "EmployeeCode",
        "FullName",
        "DepartmentId",
        "DepartmentName",
        "PositionName",
        "DateOfBirth",
        "IdentityNumber",
        "IdentityDate",
        "IdentityPlace",
        "Address",
        "PhoneNumber",
        "PhoneLandline",
        "Email",
        "BankAccount",
        "BankName",
        "BankBranch",
      ],
      // Khai báo đối tượng employee
      employee: {},
      // Khai báo danh sách đơn vị tìm kiếm
      listDepartmentSearch: [],
      // Khai báo trạng thái hiển thị của dialog cảnh báo dữ liệu k được để trống
      isShowDialogDataNotNull: false,
      // Khai báo biến xác định nội dung trường nào k được để trống
      dataNotNull: [],
      // Khai báo trạng thái hiển thị của dialog cảnh báo mã nhân viên đã tồn tại
      isShowDialogCodeExist: false,
      // Khai báo biến xác định thông tin của mã nhân viên đã tồn tại
      contentEmployeeCodeExist: "",
      // Khai báo biến quy định trang thái hiển thị dialog dữ liệu đã bị thay đổi
      isShowDialogDataChange: false,
      // Khai báo biến xác định border red
      isBorderRed: {},
      // Khai báo biến quy định sau 1 khoảng thời gian mới thực hiện tìm kiếm ở combobox
      searchDepartmentTimeout: null,
      // Khai báo biên lưu mã nhân viên tự động sinh ra
      newEmployeeCode: null,
      // Khai báo biến lưu title form mode
      titleFormMode: this.$_MSResource[this.$_LANG_CODE].FORM.ADD_EMPLOYEE,
      // Khai báo biến chứa danh sách đối tượng lỗi
      errors: {},
      // Khai báo biến chứa danh sách các ô input khi hover
      isHovering: {},
    };
  },

  computed: {
    /**
     * Mô tả: Hàm tính toán ngày sinh nhật
     * created by : BNTIEN
     * created date: 01-06-2023 02:41:01
     */
    formattedDate: {
      get() {
        if (this.employee.DateOfBirth) {
          const isoDate = this.employee.DateOfBirth;
          const formattedDate = isoDate.split(this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SPLIT_DATE)[0];
          return formattedDate;
        }
        return "";
      },
      set(newDate) {
        this.employee.DateOfBirth = newDate;
      },
    },

    /**
     * Mô tả: Hàm tính toán ngày cấp chứng minh nhân dân
     * created by : BNTIEN
     * created date: 01-06-2023 02:41:35
     */
    formattedDateIdentity: {
      get() {
        if (this.employee.IdentityDate) {
          const isoDateIdentity = this.employee.IdentityDate;
          const formattedDateIdentity = isoDateIdentity.split(
            this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SPLIT_DATE
          )[0];
          return formattedDateIdentity;
        }
        return "";
      },
      set(newDate) {
        this.employee.IdentityDate = newDate;
      },
    },
  },

  // watch: {
  //   'employee.DateOfBirth': function() {
  //     this.isBorderRed.DateOfBirth = false;
  //     this.isHovering.DateOfBirth = false;
  //   },

  //   'employee.IdentityDate': function() {
  //     this.isBorderRed.IdentityDate = false;
  //     this.isHovering.IdentityDate = false;
  //   }
  // },

  methods: {
    /**
     * Mô tả: Lấy nhân viên có giá trị lớn nhất trong hệ thống
     * created by : BNTIEN
     * created date: 24-06-2023 09:57:13
     */
    async getNewCode() {
      try {
        let maxEmployeeCode = await employeeService.getCodeMax();
        this.newEmployeeCode = maxEmployeeCode.data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm lấy danh sách department từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListDepartment() {
      try {
        const res = await departmentService.getByName("");
        this.listDepartmentSearch = res.data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: gọi api lấy dữ liệu
     * created by : BNTIEN
     * created date: 30-05-2023 14:57:33
     */
    async loadData() {
      try {
        await this.getListDepartment();
        await this.getNewCode();
        // Nếu form ở trạng thái thêm mới
        // Chuyển đối tượng sang chuỗi json
        let res = JSON.stringify(this.employeeSelected);
        // Chuyển đổi chuỗi json thành đối tượng employee
        this.employee = JSON.parse(res);
        if (this.statusFormMode !== this.$_MSEnum.FORM_MODE.Edit) {
          // Sinh mã tự động
          this.employee.EmployeeCode = this.newEmployeeCode;
          // Gán title cho form mode thêm mới
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.ADD_EMPLOYEE;
        } else {
          // Gán title cho form mode thêm sửa
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.UPDATE_EMPLOYEE;
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm focus vào ô input mã nhân viên
     * created by : BNTIEN
     * created date: 27-06-2023 01:53:48
     */
    focusCode() {
      this.$refs.EmployeeCode.focus();
    },
    /**
     * Mô tả: Xử lí chọn và bỏ chọn ô checkbox customer
     * created by : BNTIEN
     * created date: 24-06-2023 09:56:15
     */
    handleCustomerCheckboxChange() {
      if (this.employee.IsCustomer === this.$_MSEnum.CUSTOMER) {
        this.employee.IsCustomer = null; // Bỏ chọn ô input nếu đã được chọn trước đó
      } else {
        this.employee.IsCustomer = this.$_MSEnum.CUSTOMER;
      }
    },
    /**
     * Mô tả: xử lí chọn và bỏ chọn ô checkbox provider
     * created by : BNTIEN
     * created date: 24-06-2023 09:56:48
     */
    handleProviderCheckboxChange() {
      if (this.employee.IsProvider === this.$_MSEnum.PROVIDER) {
        this.employee.IsProvider = null; // Bỏ chọn ô input nếu đã được chọn trước đó
      } else {
        this.employee.IsProvider = this.$_MSEnum.PROVIDER;
      }
    },

    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search department và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChange(newValue) {
      this.isBorderRed.DepartmentName = false;
      this.isBorderRed.DepartmentId = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchDepartmentTimeout);
        this.employee.DepartmentName = newValue;
        delete this.employee.DepartmentId;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchDepartmentTimeout = setTimeout(async () => {
          const newListDepartment = await departmentService.getByName(newValue);
          this.listDepartmentSearch = newListDepartment.data;
        }, 500);
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Hàm kiểm tra xem employee có thay đổi sau khi mở form detail không
     * created by : BNTIEN
     * created date: 30-06-2023 00:21:22
     */
    hasDataChanged() {
      return JSON.stringify(this.employeeSelected) !== JSON.stringify(this.employee);
    },
    /**
     * Mô tả: Hàm sử lí sự kiện khi click vào icon close
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:28
     */
    async onCloseFormDetail() {
      // Kiểm tra xem giữ liệu có thay đổi hay không (Trường hợp có thay đổi)
      if (this.hasDataChanged()) {
        this.isShowDialogDataChange = true;
      } else {
        this.$emit("closeFormDetail");
      }
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedDepartment(department) {
      this.employee.DepartmentName = department.DepartmentName;
      this.employee.DepartmentId = department.DepartmentId;
      this.isBorderRed.DepartmentName = false;
    },
    /**
     * Mô tả: Hàm set các lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 16:36:05
     */
    setError(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].VALIDATE[key];
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE[key]);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm set các lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 16:36:05
     */
    setErrorMaxLength(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[key].Warning;
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[key].Warning);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Validate lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 10:07:22
     */
    validateEmployee() {
      try {
        for (const refInput of this.employeeProperty) {
          switch (refInput) {
            case "EmployeeCode":
            case "FullName":
              if (helperCommon.isEmptyInput(this.employee[refInput])) {
                this.setError(refInput);
              } else if (
                helperCommon.isMaxLengthInput(
                  this.employee[refInput],
                  this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                )
              ) {
                this.setErrorMaxLength(refInput);
              }
              break;
            case "DepartmentId":
              break;
            case "DepartmentName":
              if (helperCommon.isEmptyInput(this.employee[refInput])) {
                this.setError(refInput);
              }
              break;
            case "DateOfBirth":
            case "IdentityDate":
              if (this.employee[refInput]) {
                if (helperCommon.isInvalidDate(this.employee[refInput])) {
                  this.setError(refInput);
                }
              }
              break;
            case "IdentityNumber":
              if (this.employee[refInput]) {
                if (
                  helperCommon.isMaxLengthInput(
                    this.employee[refInput],
                    this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                  )
                ) {
                  this.setErrorMaxLength(refInput);
                } else if (helperCommon.isNumber(this.employee[refInput])) {
                  this.setError(refInput);
                }
              }
              break;
            case "Email":
              if (this.employee[refInput]) {
                if (
                  helperCommon.isMaxLengthInput(
                    this.employee[refInput],
                    this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                  )
                ) {
                  this.setErrorMaxLength(refInput);
                } else if (helperCommon.isFormatEmail(this.employee[refInput])) {
                  this.setError(refInput);
                }
              }
              break;
            default:
              if (this.employee[refInput]) {
                if (
                  helperCommon.isMaxLengthInput(
                    this.employee[refInput],
                    this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                  )
                ) {
                  this.setErrorMaxLength(refInput);
                }
              }
              break;
          }
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lí lỗi nhập liệu người dùng khi backend trả về lỗi
     * created by : BNTIEN
     * created date: 29-06-2023 07:07:16
     */
    handleErrorInputEmployee(errors, employeeProperty) {
      const responseHandle = helperCommon.handleErrorInput(errors, employeeProperty);
      this.errors = responseHandle.error;
      this.isBorderRed = responseHandle.isBorderRed;
      this.dataNotNull = responseHandle.dataNotNull;
      if (this.dataNotNull.length > 0) {
        this.isShowDialogDataNotNull = true;
      }
    },
    /**
     * Mô tả: Hàm kiểm tra xem mã nhân viên đã tồn tại trong database hay chưa
     * created by : BNTIEN
     * created date: 29-06-2023 23:46:11
     */
    async checkEmployeeExists() {
      try {
        const res = await employeeService.getByCode(this.employee.EmployeeCode);
        return res.data;
      } catch {
        return null;
      }
    },
    /**
     * Mô tả: Hàm xử lí khi mã nhân viên đã tồn tại trong hệ thống
     * created by : BNTIEN
     * created date: 30-06-2023 00:30:22
     */
    handleEmployeeExisted(employeeExisted) {
      this.isShowDialogCodeExist = true;
      this.isBorderRed.EmployeeCode = true;
      this.errors["EmployeeCode"] = `${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_PRE}
       ${employeeExisted.EmployeeCode} ${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_DETAIL_END}`;
      this.contentEmployeeCodeExist = employeeExisted.EmployeeCode;
    },
    /**
     * Mô tả: Hàm xử lí sự kiện khi người dùng bấm vào nút cất trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:05
     */
    async btnSave() {
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateEmployee();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
            let employeeByCode = await this.checkEmployeeExists();
            if (!employeeByCode) {
              // Nếu mã nhân viên chưa tồn tại trong hệ thống
              const res = await employeeService.create(this.employee);
              if (this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.status) && res.data > 0) {
                this.$_MSEmitter.emit(
                  "onShowToastMessage",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
                );
                this.$emit("closeFormDetail");
                this.$_MSEmitter.emit("refreshDataTable");
              }
            } else {
              // Nếu mã nhân viên đã tồn tại trong hệ thống
              this.handleEmployeeExisted(employeeByCode);
            }
          } catch (error) {
            this.handleErrorInputEmployee(error, this.employeeProperty);
          }
        }
      } else {
        // Nếu form ở trạng thái sửa
        // Kiểm tra xem dữ liệu có thay đổi hay k (Trường hợp đã thay đổi)
        if (this.hasDataChanged()) {
          this.validateEmployee();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
              let employeeByCode = await this.checkEmployeeExists();
              // Nếu mã nhân viên chưa tồn tại trong hệ thống hoặc tồn tại nhưng trùng với nhân viên đang sửa
              if (!employeeByCode || employeeByCode.EmployeeCode === this.employeeSelected.EmployeeCode) {
                const res = await employeeService.update(this.employeeSelected.EmployeeId, this.employee);
                if (this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.status) && res.data > 0) {
                  this.$_MSEmitter.emit(
                    "onShowToastMessageUpdate",
                    this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                  );
                  this.$emit("closeFormDetail");
                  this.$_MSEmitter.emit("refreshDataTable");
                }
              } else {
                // Nếu mã nhân viên đã tồn tại trong hệ thống
                this.handleEmployeeExisted(employeeByCode);
              }
            } catch (error) {
              this.handleErrorInputEmployee(error, this.employeeProperty);
            }
          }
        } else {
          this.$emit("closeFormDetail");
        }
      }
      // }
    },
    /**
     * Mô tả: Hàm xử lí sự kiện khi người dùng bấm vào nut cất và thêm trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:23
     */
    async btnSaveAndAdd() {
      // Nếu form ở trạng thái thêm mới
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateEmployee();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
            let employeeByCode = await this.checkEmployeeExists();
            if (!employeeByCode) {
              // Nếu mã nhân viên chưa tồn tại trong hệ thống
              const res = await employeeService.create(this.employee);
              if (this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.status) && res.data > 0) {
                this.$_MSEmitter.emit(
                  "onShowToastMessage",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
                );
                this.employee = {};
                this.isBorderRed = {};
                this.$_MSEmitter.emit("refreshDataTable");
                await this.getNewCode();
                this.employee.EmployeeCode = this.newEmployeeCode;
                this.focusCode();
              }
            } else {
              // Nếu mã nhân viên đã tồn tại trong hệ thống
              this.handleEmployeeExisted(employeeByCode);
            }
          } catch (error) {
            this.handleErrorInputEmployee(error, this.employeeProperty);
          }
        }
        // Nếu form ở trạng thái sửa
      } else {
        // Kiểm tra xem dữ liệu có thay đổi hay k
        if (this.hasDataChanged()) {
          this.validateEmployee();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
              let employeeByCode = await this.checkEmployeeExists();
              // Nếu mã nhân viên chưa tồn tại trong hệ thống hoặc tồn tại trùng với nhân viên đang sửa
              if (!employeeByCode || employeeByCode.EmployeeCode === this.employeeSelected.EmployeeCode) {
                const res = await employeeService.update(this.employeeSelected.EmployeeId, this.employee);
                this.employee = {};
                this.$_MSEmitter.emit("setFormModeAdd");
                await this.getNewCode();
                this.employee.EmployeeCode = this.newEmployeeCode;
                this.focusCode();
                this.$_MSEmitter.emit("refreshDataTable");
                if (this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.status) && res.data > 0) {
                  this.$_MSEmitter.emit(
                    "onShowToastMessageUpdate",
                    this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                  );
                }
              } else {
                // Nếu mã nhân viên đã tồn tại trong hệ thống
                this.handleEmployeeExisted(employeeByCode);
              }
            } catch (error) {
              this.handleErrorInputEmployee(error, this.employeeProperty);
            }
          }
        } else {
          this.employee = {};
          this.$_MSEmitter.emit("setFormModeAdd");
          this.focusCode();
        }
      }
    },
    /**
     * Mô tả: Hàm đóng dialog cảnh báo dữ liệu k được để trống và focus vào các ô trống
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:59
     */
    onCloseDialogSaveAndAdd() {
      this.isShowDialogDataNotNull = false;
      this.dataNotNull = [];
      let listPropError = [];
      for (const key in this.isBorderRed) {
        if (this.isBorderRed[key] === true) {
          listPropError.push(key);
        }
      }
      // thêm thuộc tính DepartmentName vào listPropError để xử lí focus nếu chưa có
      if (listPropError.includes("DepartmentId") && !listPropError.includes("DepartmentName")) {
        listPropError.push("DepartmentName");
      }
      for (const prop of this.employeeProperty) {
        if (listPropError.includes(prop)) {
          // đợi DOM cập nhật trước khi thực thi focus
          if (prop === "DepartmentId" || prop === "DepartmentName") {
            this.$nextTick(() => {
              this.$_MSEmitter.emit("focusInputCBB");
            });
          } else {
            this.$nextTick(() => {
              this.$refs[prop].focus();
            });
          }
          return;
        }

        // else if(prop === "DateOfBirth" || prop === "IdentityDate"){
        //     this.$nextTick(() => {
        //       this.$refs[prop].$el.focus();
        //     });
        //   }
      }
    },

    /**
     * Mô tả: Hàm bỏ border red khi dữ liệu thay đổi
     * created by : BNTIEN
     * created date: 29-06-2023 22:03:38
     */
    setIsBorderRed(prop) {
      if (prop in this.isBorderRed) {
        this.isBorderRed[prop] = false;
      }
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi click vào nút hủy trong form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:20
     */
    btnCancel() {
      this.$emit("closeFormDetail");
    },
    /**
     * Mô tả: Hàm xử lí sự kiện đóng dialog cảnh báo mã nhân viên đã tồn tại
     * created by : BNTIEN
     * created date: 29-05-2023 08:28:19
     */
    btnCloseDialogCodeExist() {
      this.isShowDialogCodeExist = false;
      this.focusCode();
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi bấm vào button hủy trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:40:13
     */
    onCancelDialogDataChange() {
      this.isShowDialogDataChange = false;
      this.focusCode();
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi bấm vào button không trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:42:10
     */
    onNoDialogDataChange() {
      this.$emit("closeFormDetail");
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi bấm vào button có trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:43:38
     */
    async onYesDialogDataChange() {
      this.isShowDialogDataChange = false;
      await this.btnSave();
    },

    /**
     * Mô tả: Hàm reset tabindex về ô input mã nhân viên khi tab nhảy đến icon close
     * created by : BNTIEN
     * created date: 01-06-2023 14:24:19
     */
    resetTab() {
      this.focusCode();
    },

    /**
     * Mô tả: xử lí sự kiện khi bấm esc khi đang ở form detail
     * created by : BNTIEN
     * created date: 01-07-2023 01:05:25
     */
    async handleKeyDown(event) {
      if (event.key === "Escape") {
        // Nếu phím được nhấn là Esc, thực hiện hàm onCloseFormDetail
        await this.onCloseFormDetail();
      } else if (event.ctrlKey && event.key === "s") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của trình duyệt khi nhấn phím Ctrl + S
        this.btnSave();
      } else if (event.ctrlKey && event.shiftKey && event.key === "S") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của trình duyệt khi nhấn tổ hợp phím Ctrl + Shift + S
        this.btnSaveAndAdd();
      }
    },
  },

  beforeUnmount() {
    this.$_MSEmitter.off("cancelDialogDataChange");
    this.$_MSEmitter.off("noDialogDataChange");
    this.$_MSEmitter.off("yesDialogDataChange");
    this.$_MSEmitter.off("closeDialogCodeExist");
    this.$_MSEmitter.off("closeDialogDataError");
    this.$_MSEmitter.off("onSelectedEntityCBB");
    this.$_MSEmitter.off("onSearchChangeCBB");
    this.$_MSEmitter.off("onKeyDownEntityCBB");
    // Xóa các sự kiện đã đăng kí
    this.$refs.FormDetail.removeEventListener("keydown", this.handleKeyDown);
  },
};
</script>

<style scoped>
@import url(@/css/detailinfoemployee.css);

.border-red {
  border: 1px solid red;
}
/* .dp__main, .dp__input_wrap, .dp__input{
  border-radius: 4px;
  height: 32px;
} */
</style>
