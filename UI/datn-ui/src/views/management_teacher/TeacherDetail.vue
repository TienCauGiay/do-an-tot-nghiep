<template>
  <div id="detail-info" class="position-display-center" ref="FormDetail">
    <div class="form-detail-toolbar">
      <!-- <div class="question-icon icon-tb" :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.HELP"></div> -->
      <div
        @click="onCloseFormDetail"
        class="close-icon icon-tb"
        id="teacher-exist"
        @keydown.tab.prevent="resetTab($event.target.value)"
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.EXIST"
      ></div>
    </div>
    <div class="form-detail-main">
      <div class="teacher-title">
        <p>
          <b>{{ this.titleFormMode }}</b>
        </p>
      </div>
      <div class="form-detail-content">
        <div class="half-content">
          <div class="col-md-n">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.TeacherCode }}
              <span class="s-require">*</span>
            </label>
            <div class="container-input">
              <ms-input
                ref="teacher_code"
                v-model="teacher.teacher_code"
                :class="{ 'border-red': isBorderRed.teacher_code }"
                @input="setIsBorderRed('teacher_code')"
                @mouseenter="isHovering.teacher_code = true"
                @mouseleave="isHovering.teacher_code = false"
                :disabled="statusFormMode === $_MSEnum.FORM_MODE.Edit"
                :maxLength="50"
              ></ms-input>
              <div
                class="ms-tooltip"
                v-if="
                  isHovering.teacher_code &&
                  (isBorderRed.teacher_code || !teacher.teacher_code) &&
                  errors['teacher_code']
                "
              >
                {{ errors["teacher_code"] }}
              </div>
            </div>
          </div>
          <div class="col-md-tb">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.TeacherName }}
              <span class="s-require">*</span>
            </label>
            <div class="container-input">
              <ms-input
                ref="teacher_name"
                v-model="teacher.teacher_name"
                :class="{ 'border-red': isBorderRed.teacher_name }"
                @input="setIsBorderRed('teacher_name')"
                @mouseenter="isHovering.teacher_name = true"
                @mouseleave="isHovering.teacher_name = false"
                :maxLength="255"
              ></ms-input>
              <div
                class="ms-tooltip"
                v-if="
                  isHovering.teacher_name &&
                  (isBorderRed.teacher_name || !teacher.teacher_name) &&
                  errors['teacher_name']
                "
              >
                {{ errors["teacher_name"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label>{{ $_MSResource[$_LANG_CODE].FORM.Gender }}</label>
            <div class="e-gender">
              <input
                v-model="teacher.gender"
                :value="$_MSResource[$_LANG_CODE].TEXT_CONTENT.GENDER.Male"
                type="radio"
                name="gender"
              />
              <span>{{ $_MSResource[$_LANG_CODE].TEXT_CONTENT.GENDER.Male }}</span>
              <input
                v-model="teacher.gender"
                :value="$_MSResource[$_LANG_CODE].TEXT_CONTENT.GENDER.Female"
                type="radio"
                name="gender"
              />
              <span>{{ $_MSResource[$_LANG_CODE].TEXT_CONTENT.GENDER.Female }}</span>
              <input
                v-model="teacher.gender"
                :value="$_MSResource[$_LANG_CODE].TEXT_CONTENT.GENDER.Other"
                type="radio"
                name="gender"
              />
              <span>{{ $_MSResource[$_LANG_CODE].TEXT_CONTENT.GENDER.Other }}</span>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label>{{ this.$_MSResource[this.$_LANG_CODE].FORM.Birthday }}</label>
            <div class="container-input">
              <ms-input
                ref="birthday"
                type="date"
                v-model="teacher.birthday"
                :value="formattedDate"
                :class="{
                  'border-red': isBorderRed.birthday,
                }"
                @input="setIsBorderRed('birthday')"
                @mouseenter="isHovering.birthday = true"
                @mouseleave="isHovering.birthday = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.birthday && isBorderRed.birthday">
                {{ errors["birthday"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemFaculty">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.Faculty }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              :isBorderRedCBB="isBorderRed"
              :entityCBB="teacher"
              :errorsCBB="errors"
              :listEntitySearchCBB="listFacultySearch"
              :propName="'faculty_name'"
              :propId="'faculty_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderFaculty"
            ></ms-combobox>
          </div>
        </div>
        <div class="full-content">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.Address }}</label>
          <div class="container-input">
            <ms-input
              ref="address"
              v-model="teacher.address"
              :class="{ 'border-red': isBorderRed.address }"
              @input="setIsBorderRed('address')"
              @mouseenter="isHovering.address = true"
              @mouseleave="isHovering.address = false"
              :maxLength="500"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.address && isBorderRed.address">
              {{ errors["address"] }}
            </div>
          </div>
        </div>
        <div class="full-content">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.PhoneNumber }}</label>
          <div class="container-input">
            <ms-input
              ref="phone_number"
              v-model="teacher.phone_number"
              :class="{ 'border-red': isBorderRed.phone_number }"
              @input="setIsBorderRed('phone_number')"
              @mouseenter="isHovering.phone_number = true"
              @mouseleave="isHovering.phone_number = false"
              :maxLength="50"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.phone_number && isBorderRed.phone_number">
              {{ errors["phone_number"] }}
            </div>
          </div>
        </div>
        <div class="full-content">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.Email }}</label>
          <div class="container-input">
            <ms-input
              ref="email"
              v-model="teacher.email"
              :class="{ 'border-red': isBorderRed.email }"
              @input="setIsBorderRed('email')"
              @mouseenter="isHovering.email = true"
              @mouseleave="isHovering.email = false"
              :maxLength="100"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.email && isBorderRed.email">
              {{ errors["email"] }}
            </div>
          </div>
        </div>
      </div>
      <div class="form-detail-action">
        <div class="action-left">
          <ms-button-extra
            :textButtonExtra="this.$_MSResource[this.$_LANG_CODE].BUTTON.CANCEL"
            @click="btnCancel"
          ></ms-button-extra>
        </div>
        <div class="action-right">
          <ms-button-default
            :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.SAVE"
            @click="btnSave"
            :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SAVE"
          ></ms-button-default>
        </div>
      </div>
    </div>
    <!-- dialog teacher input data not blank -->
    <ms-dialog-data-not-null
      v-if="isShowDialogDataNotNull"
      :valueNotNull="dataNotNull"
      :title="this.$_MSResource[this.$_LANG_CODE].DIALOG.TITLE.DATA_INVALID"
    ></ms-dialog-data-not-null>
    <!-- dialog teacher id Exist -->
    <ms-dialog-data-exist
      v-if="isShowDialogCodeExist"
      :textProp="this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_PRE"
      :textEntityCodeExist="contentTeacherCodeExist"
    ></ms-dialog-data-exist>
    <!-- dialog teacher save and close -->
    <ms-dialog-data-change v-if="isShowDialogDataChange"></ms-dialog-data-change>
  </div>
</template>

<script>
import teacherService from "@/services/teacher.js";
import facultyService from "@/services/faculty.js";
import helperCommon from "@/helpers/helper.js";

export default {
  name: "TeacherDetail",

  props: ["teacherSelected", "statusFormMode"],

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
      this.onSelectedFaculty(data);
    });
    this.$_MSEmitter.on("onSearchChangeCBB", (newValue) => {
      this.onSearchChange(newValue);
    });
    this.$_MSEmitter.on("onKeyDownEntityCBB", (index) => {
      this.teacher.faculty_name = this.listFacultySearch[index].faculty_name;
      this.teacher.faculty_id = this.listFacultySearch[index].faculty_id;
      this.isBorderRed.faculty_name = false;
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
      teacherProperty: [
        "teacher_code",
        "teacher_name",
        "gender",
        "birthday",
        "faculty_id",
        "faculty_name",
        "address",
        "phone_number",
        "email",
      ],
      // Khai báo đối tượng teacher
      teacher: {},
      // Khai báo danh sách đơn vị tìm kiếm
      listFacultySearch: [],
      // Khai báo trạng thái hiển thị của dialog cảnh báo dữ liệu k được để trống
      isShowDialogDataNotNull: false,
      // Khai báo biến xác định nội dung trường nào k được để trống
      dataNotNull: [],
      // Khai báo trạng thái hiển thị của dialog cảnh báo mã nhân viên đã tồn tại
      isShowDialogCodeExist: false,
      // Khai báo biến xác định thông tin của mã nhân viên đã tồn tại
      contentTeacherCodeExist: "",
      // Khai báo biến quy định trang thái hiển thị dialog dữ liệu đã bị thay đổi
      isShowDialogDataChange: false,
      // Khai báo biến xác định border red
      isBorderRed: {},
      // Khai báo biến quy định sau 1 khoảng thời gian mới thực hiện tìm kiếm ở combobox
      searchTimeout: null,
      // Khai báo biến lưu title form mode
      titleFormMode: this.$_MSResource[this.$_LANG_CODE].FORM.AddTeacher,
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
        if (this.teacher.birthday) {
          const isoDate = this.teacher.birthday;
          const formattedDate = isoDate.split(this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SPLIT_DATE)[0];
          return formattedDate;
        }
        return "";
      },
      set(newDate) {
        this.teacher.birthday = newDate;
      },
    },
  },

  methods: {
    /**
     * Mô tả: Hàm lấy danh sách faculty từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListFaculty() {
      try {
        const res = await facultyService.search("");
        this.listFacultySearch = res.data.Data;
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
        await this.getListFaculty();
        // Nếu form ở trạng thái thêm mới
        // Chuyển đối tượng sang chuỗi json
        let res = JSON.stringify(this.teacherSelected);
        // Chuyển đổi chuỗi json thành đối tượng teacher
        this.teacher = JSON.parse(res);
        if (this.statusFormMode !== this.$_MSEnum.FORM_MODE.Edit) {
          // Gán title cho form mode thêm mới
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.AddTeacher;
        } else {
          // Gán title cho form mode thêm sửa
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.UpdateTeacher;
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
      this.$refs.teacher_code.focus();
    },

    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search faculty và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChange(newValue) {
      this.isBorderRed.faculty_name = false;
      this.isBorderRed.faculty_id = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchTimeout);
        this.teacher.faculty_name = newValue;
        delete this.teacher.faculty_id;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchTimeout = setTimeout(async () => {
          const newListFaculty = await facultyService.search(newValue);
          this.listFacultySearch = newListFaculty.data.Data;
        }, 500);
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Hàm kiểm tra xem teacher có thay đổi sau khi mở form detail không
     * created by : BNTIEN
     * created date: 30-06-2023 00:21:22
     */
    hasDataChanged() {
      return JSON.stringify(this.teacherSelected) !== JSON.stringify(this.teacher);
    },
    /**
     * Mô tả: Hàm sử lý sự kiện khi click vào icon close
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
     * Mô tả: Hàm xử lý sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedFaculty(faculty) {
      this.teacher.faculty_name = faculty.faculty_name;
      this.teacher.faculty_id = faculty.faculty_id;
      this.isBorderRed.faculty_name = false;
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
    validateTeacher() {
      try {
        for (const refInput of this.teacherProperty) {
          switch (refInput) {
            case "teacher_code":
            case "teacher_name":
              if (helperCommon.isEmptyInput(this.teacher[refInput])) {
                this.setError(refInput);
              } else if (
                helperCommon.isMaxLengthInput(
                  this.teacher[refInput],
                  this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                )
              ) {
                this.setErrorMaxLength(refInput);
              }
              break;
            case "faculty_name":
              if (helperCommon.isEmptyInput(this.teacher[refInput])) {
                this.setError(refInput);
              } else {
                if (!this.teacher.faculty_id) {
                  this.errors.faculty_name = this.$_MSResource[this.$_LANG_CODE].VALIDATE.faculty_id;
                  this.isBorderRed.faculty_name = true;
                  this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE.faculty_id);
                }
              }
              break;
            case "birthday":
              if (this.teacher[refInput]) {
                if (helperCommon.isInvalidDate(this.teacher[refInput])) {
                  this.setError(refInput);
                }
              }
              break;
            case "email":
              if (this.teacher[refInput]) {
                if (
                  helperCommon.isMaxLengthInput(
                    this.teacher[refInput],
                    this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                  )
                ) {
                  this.setErrorMaxLength(refInput);
                } else if (helperCommon.isNotFormatEmail(this.teacher[refInput])) {
                  this.setError(refInput);
                }
              }
              break;
            case "phone_number":
              if (this.teacher[refInput]) {
                if (helperCommon.isNotNumber(this.teacher[refInput])) {
                  this.errors.phone_number = this.$_MSResource[this.$_LANG_CODE].VALIDATE.phone_number;
                  this.isBorderRed.phone_number = true;
                  this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE.phone_number);
                }
              }
              break;
            default:
              break;
          }
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý lỗi nhập liệu người dùng khi backend trả về lỗi
     * created by : BNTIEN
     * created date: 29-06-2023 07:07:16
     */
    handleErrorInputTeacher(errors, teacherProperty) {
      const responseHandle = helperCommon.handleErrorInput(errors, teacherProperty);
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
    async checkTeacherExists() {
      try {
        const res = await teacherService.getByCode(this.teacher.teacher_code);
        return res.data;
      } catch {
        return null;
      }
    },
    /**
     * Mô tả: Hàm xử lý khi mã nhân viên đã tồn tại trong hệ thống
     * created by : BNTIEN
     * created date: 30-06-2023 00:30:22
     */
    handleTeacherExisted(teacherExisted) {
      this.isShowDialogCodeExist = true;
      this.isBorderRed.teacher_code = true;
      this.errors["teacher_code"] = `${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_PRE}
       ${teacherExisted.teacher_code} ${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_DETAIL_END}`;
      this.contentTeacherCodeExist = teacherExisted.teacher_code;
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng bấm vào nút cất trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:05
     */
    async btnSave() {
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateTeacher();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
            let teacherByCode = (await this.checkTeacherExists()).Data;
            if (!teacherByCode) {
              // Nếu mã nhân viên chưa tồn tại trong hệ thống
              const res = await teacherService.create(this.teacher);
              if (
                res &&
                res.data &&
                this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.data.Code) &&
                res.data.Data > 0
              ) {
                this.$_MSEmitter.emit(
                  "onShowToastMessage",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
                );
                this.$emit("closeFormDetail");
                this.$_MSEmitter.emit("refreshDataTable");
              }
            } else {
              // Nếu mã nhân viên đã tồn tại trong hệ thống
              this.handleTeacherExisted(teacherByCode);
            }
          } catch (error) {
            this.handleErrorInputTeacher(error, this.teacherProperty);
          }
        }
      } else {
        // Nếu form ở trạng thái sửa
        // Kiểm tra xem dữ liệu có thay đổi hay k (Trường hợp đã thay đổi)
        if (this.hasDataChanged()) {
          this.validateTeacher();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
              let teacherByCode = (await this.checkTeacherExists()).Data;
              // Nếu mã nhân viên chưa tồn tại trong hệ thống hoặc tồn tại nhưng trùng với nhân viên đang sửa
              if (!teacherByCode || teacherByCode.teacher_code === this.teacherSelected.teacher_code) {
                const res = await teacherService.update(this.teacher);
                if (
                  res &&
                  res.data &&
                  this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) &&
                  res.data.Data > 0
                ) {
                  this.$_MSEmitter.emit(
                    "onShowToastMessageUpdate",
                    this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                  );
                  this.$emit("closeFormDetail");
                  this.$_MSEmitter.emit("refreshDataTable");
                }
              } else {
                // Nếu mã nhân viên đã tồn tại trong hệ thống
                this.handleTeacherExisted(teacherByCode);
              }
            } catch (error) {
              this.handleErrorInputTeacher(error, this.teacherProperty);
            }
          }
        } else {
          this.$emit("closeFormDetail");
        }
      }
      // }
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng bấm vào nut cất và thêm trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:23
     */
    async btnSaveAndAdd() {
      // Nếu form ở trạng thái thêm mới
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateTeacher();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
            let teacherByCode = (await this.checkTeacherExists()).Data;
            if (!teacherByCode) {
              // Nếu mã nhân viên chưa tồn tại trong hệ thống
              const res = await teacherService.create(this.teacher);
              if (
                res &&
                res.data &&
                this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.data.Code) &&
                res.data.Data > 0
              ) {
                this.$_MSEmitter.emit(
                  "onShowToastMessage",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
                );
                this.teacher = {};
                this.isBorderRed = {};
                this.$_MSEmitter.emit("refreshDataTable");
                this.focusCode();
              }
            } else {
              // Nếu mã nhân viên đã tồn tại trong hệ thống
              this.handleTeacherExisted(teacherByCode);
            }
          } catch (error) {
            this.handleErrorInputTeacher(error, this.teacherProperty);
          }
        }
        // Nếu form ở trạng thái sửa
      } else {
        // Kiểm tra xem dữ liệu có thay đổi hay k
        if (this.hasDataChanged()) {
          this.validateTeacher();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              // Kiểm tra xem mã nhân viên đã tồn tại trong database chưa, nếu đã tồn tại thì thông báo cho người dùng
              let teacherByCode = (await this.checkTeacherExists()).Data;
              // Nếu mã nhân viên chưa tồn tại trong hệ thống hoặc tồn tại trùng với nhân viên đang sửa
              if (!teacherByCode || teacherByCode.teacher_code === this.teacherSelected.teacher_code) {
                const res = await teacherService.update(this.teacher);
                this.teacher = {};
                this.$_MSEmitter.emit("setFormModeAdd");
                this.focusCode();
                this.$_MSEmitter.emit("refreshDataTable");
                if (
                  res &&
                  res.data &&
                  this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) &&
                  res.data.Data > 0
                ) {
                  this.$_MSEmitter.emit(
                    "onShowToastMessageUpdate",
                    this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                  );
                }
              } else {
                // Nếu mã nhân viên đã tồn tại trong hệ thống
                this.handleTeacherExisted(teacherByCode);
              }
            } catch (error) {
              this.handleErrorInputTeacher(error, this.teacherProperty);
            }
          }
        } else {
          this.teacher = {};
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
      // thêm thuộc tính faculty_name vào listPropError để xử lý focus nếu chưa có
      if (listPropError.includes("faculty_id") && !listPropError.includes("faculty_name")) {
        listPropError.push("faculty_name");
      }
      for (const prop of this.teacherProperty) {
        if (listPropError.includes(prop)) {
          // đợi DOM cập nhật trước khi thực thi focus
          if (prop === "faculty_id" || prop === "faculty_name") {
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
     * Mô tả: Hàm xử lý sự kiện khi click vào nút hủy trong form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:20
     */
    btnCancel() {
      this.$emit("closeFormDetail");
    },
    /**
     * Mô tả: Hàm xử lý sự kiện đóng dialog cảnh báo mã nhân viên đã tồn tại
     * created by : BNTIEN
     * created date: 29-05-2023 08:28:19
     */
    btnCloseDialogCodeExist() {
      this.isShowDialogCodeExist = false;
      this.focusCode();
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi bấm vào button hủy trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:40:13
     */
    onCancelDialogDataChange() {
      this.isShowDialogDataChange = false;
      this.focusCode();
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi bấm vào button không trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:42:10
     */
    onNoDialogDataChange() {
      this.$emit("closeFormDetail");
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi bấm vào button có trong dialog dữ liệu đã bị thay đổi
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
     * Mô tả: xử lý sự kiện khi bấm esc khi đang ở form detail
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
@import url(@/css/detailinfo.css);

.border-red {
  border: 1px solid red;
}
/* .dp__main, .dp__input_wrap, .dp__input{
  border-radius: 4px;
  height: 32px;
} */
</style>
