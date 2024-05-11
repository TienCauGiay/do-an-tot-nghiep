<template>
  <div class="content-title">
    <h1>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ManagementTeacher }}</h1>
  </div>
  <div class="content-main-body">
    <div class="content-action">
      <div class="content-action-left">
        <button
          :disabled="isDisableExcuteBatch"
          class="delete-multiple"
          @click="onShowExcuteBatch"
          :class="{ 'no-disable': !isDisableExcuteBatch }"
          ref="DeleteMulti"
        >
          <div class="select-function-delete">
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.EXCUTE_BATCH }}</span>
            <div class="delete-multiple-icon">
              <div class="function-icon-disable"></div>
            </div>
          </div>
          <div class="menu-delete" v-show="isShowMenuExcuteBatch">
            <div class="menu-item-delete" @click="onShowDialogDeleteMulti">
              {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.DELETE }}
            </div>
          </div>
        </button>
        <ms-select-option
          :listData="listOptionFilter"
          :propCode="'option_code'"
          :propName="'option_name'"
          :entity="optionFilter"
          :placeholderValue="this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SelectFilterType"
          :indexSelect="listOptionFilter.findIndex((obj) => obj.option_code == optionFilter.option_code)"
          :isReadonly="true"
        ></ms-select-option>
        <ms-select-option
          :listData="listConditionFilter"
          :propCode="'condition_code'"
          :propName="'condition_name'"
          :entity="conditionFilter"
          :customStyle="'width: 250px;'"
          :placeholderValue="this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SelectFilterCondition"
          :indexSelect="listConditionFilter.findIndex((obj) => obj.condition_code == conditionFilter.condition_code)"
          :isDisabledMenu="isDisabledMenuConditionFilter"
        ></ms-select-option>
        <div class="delete-filter" @click="deleteFilterCondition">
          {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.DeleteFilterCondition }}
        </div>
      </div>
      <div class="search-entity">
        <input
          type="search"
          :placeholder="this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PLACEHOLDER_SEARCH"
          v-model="textSearch"
          @keydown.enter="onSearchTeacher"
          @input="autoSearch"
        />
        <div class="search-icon icon-tb" @click="onSearchTeacher"></div>
      </div>
      <div
        @click="refreshData"
        class="refresh-icon icon-tb"
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.REFRESH"
      ></div>
      <div
        @click="exportExcel"
        class="excel-icon icon-tb"
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.EXCEL"
      ></div>
      <!-- <div class="setting-icon icon-tb" :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SETTING_MAIN"></div> -->
      <div class="insert-data">
        <ms-button-default
          :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ADD"
          @click="btnOpenFormDetail"
        ></ms-button-default>
        <ms-button-icon @importExcel="importExcel"></ms-button-icon>
      </div>
    </div>
    <div id="list-teacher" class="list-entity">
      <form action="">
        <table id="tbTeacherList">
          <thead>
            <tr>
              <th type="checkbox" class="entity-border-left">
                <div class="th-checkbox">
                  <input class="checkbox-select-row" type="checkbox" @click="checkAllSelect" :checked="isCheckAll" />
                </div>
              </th>
              <th class="e-id">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.TeacherCode }}
              </th>
              <th class="e-fullname">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.TeacherName }}
              </th>
              <!-- <th class="e-id">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.SubjectCode }}
              </th>
              <th class="e-fullname">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.SubjectName }}
              </th> -->
              <th class="e-gender-table">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.Gender }}
              </th>
              <th type="date" class="text-center e-birthday">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.Birthday }}
              </th>
              <th class="e-bank-account">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.PhoneNumber }}
              </th>
              <th>
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.Address }}
              </th>
              <th class="e-bank-branch">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.Email }}
              </th>
              <th type="feat" class="text-center entity-border-right e-birthday">
                {{ this.$_MSResource[this.$_LANG_CODE].Teacher_Column.Feature }}
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-show="dataTable.TotalRecord"
              v-for="item in dataTable.Data"
              :key="item.teacher_id"
              @dblclick="onUpdateFormDetail(item)"
              :class="{ checkedRow: checkRow().includes(item.teacher_id) }"
            >
              <td class="entity-border-left" @dblclick.stop>
                <div class="th-checkbox">
                  <input
                    class="checkbox-select-row"
                    type="checkbox"
                    @click="checkRow(item.teacher_id)"
                    :checked="checkRow().includes(item.teacher_id)"
                  />
                </div>
              </td>
              <td class="e-id" :title="item.teacher_code">
                {{ item.teacher_code }}
              </td>
              <td class="e-fullname" :title="item.teacher_name">
                {{ item.teacher_name }}
              </td>
              <!-- <td class="e-id" :title="item.subject_code">
                {{ item.subject_code }}
              </td>
              <td class="e-fullname" :title="item.subject_name">
                {{ item.subject_name }}
              </td> -->
              <td class="e-gender-table">
                {{ item.gender }}
              </td>
              <td class="text-center e-birthday">
                {{ formatDate(item.birthday) }}
              </td>
              <td class="e-bank-account" :title="item.phone_number">
                {{ item.phone_number }}
              </td>
              <td :title="item.address">
                {{ item.address }}
              </td>
              <td class="e-bank-branch" :title="item.email">
                {{ item.email }}
              </td>
              <td class="text-center entity-border-right e-birthday function-table" @dblclick.stop>
                <span @click="onUpdateFormDetail(item)">
                  {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.UPDATE }}
                </span>
                <div class="function-table-content" @click="onOpenFeatureMenu($event, item)">
                  <div class="function-icon-table function-icon-select"></div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </form>
      <teleport to="#app">
        <div
          class="menu-function-select"
          v-show="isShowColFeature"
          :style="{
            left: `${this.positionFeatureMenu.left}px`,
            top: `${this.positionFeatureMenu.top}px`,
          }"
        >
          <div @click="onDupliCateTeacher">
            {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.DUPLICATE }}
          </div>
          <div @click="onDeleteTeacher">
            {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.DELETE }}
          </div>
        </div>
      </teleport>
      <img
        v-show="isShowLoading && this.dataTable.TotalRecord !== undefined"
        class="loading"
        :class="{ 'loadding-form-detail': isShowFormDetail }"
        src="../../assets/img/loading.svg"
        alt="loading"
      />
      <div v-if="!this.dataTable.TotalRecord || this.dataTable.TotalRecord === 0" class="no-data">
        {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.NO_DATA }}
      </div>
    </div>
    <div id="pagination" class="pagination">
      <p>
        {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.TOTAL }}:
        <b>{{ this.dataTable.TotalRecord ? this.dataTable.TotalRecord : 0 }}</b>
        {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.RECORD }}
      </p>
      <div class="pagination-detail">
        <div class="pagination-detail-pagesize" :class="{ 'active-record': isShowPaging }" ref="PagingMenu">
          <div id="pagination-detail-pagesize-content" class="pagination-detail-pagesize-content">
            {{ selectedRecord }}
            {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.RECORD_ON_PAGE }}
          </div>
          <div id="menu-paging-select" class="menu-paging-select" @click="onShowSelectPaging">
            <div class="function-icon" :class="{ 'rotate-function-icon': isShowPaging }"></div>
            <ul id="menu-paging" class="menu-paging" v-show="isShowPaging">
              <li
                class="menu-paging-record"
                v-for="(record, index) in recordOptions"
                :key="index"
                @click="onSelectedRecord(record, index)"
                :class="{ 'active-record-item': indexSelectedRecord === index }"
              >
                {{ record }}
                {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.RECORD_ON_PAGE }}
              </li>
            </ul>
          </div>
        </div>
        <div class="pagination-page-number">
          <ul class="page-number">
            <button
              @click="goToPage(this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PAGE.PREVIOUS)"
              :disabled="isFirstPage"
            >
              {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PAGING_PRE }}
            </button>
            <button
              v-for="pageNumber in this.visiblePageNumbers"
              :key="pageNumber"
              @click="goToPage(pageNumber)"
              :class="{ 'active-page': pageNumber === this.currentPage }"
            >
              {{ pageNumber }}
            </button>
            <button
              @click="goToPage(this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PAGE.NEXT)"
              :disabled="isLastPage"
            >
              {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PAGING_NEXT }}
            </button>
          </ul>
        </div>
      </div>
    </div>
    <div v-if="isOverlay" id="container-overlay" class="container-overlay" @closeFormDetail="onCloseFormDetail"></div>
    <!-- teacher detail -->
    <TeacherDetail
      v-if="isShowFormDetail"
      @closeFormDetail="onCloseFormDetail"
      :teacherSelected="teacherUpdate"
      :statusFormMode="isStatusFormMode"
    ></TeacherDetail>
    <!-- dialog teacher confirm delete -->
    <ms-dialog-confirm-delete
      :isDeleteMultiple="isDeleteMultipleDialog"
      :contentDeleteMultiple="this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.CONFIRM_DELETE_MULTIPLE"
      :contentDelete="`${
        this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.CONFIRM_DELETE_PRE
      }${teacherCodeDeleteSelected}${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.END}`"
      v-if="isShowDialogConfirmDelete"
    ></ms-dialog-confirm-delete>
    <!-- toast message -->
    <ms-toast-success v-if="isShowToastMessage" :contentToast="contentToastSuccess"></ms-toast-success>
    <a href="" ref="ExportTeacher" v-show="false"></a>
  </div>
</template>

<script>
import TeacherDetail from "./TeacherDetail.vue";
import helperCommon from "@/helpers/helper.js";
import teacherService from "@/services/teacher.js";

export default {
  name: "ManagementTeacher",

  components: {
    TeacherDetail,
  },

  created() {
    // Lấy danh sách sinh viên (theo phân trang)
    this.getDataTeacher();
    // Đăng kí các sự kiện
    this.$_MSEmitter.on("onShowToastMessage", (data) => {
      this.contentToastSuccess = data;
      this.onShowToastMessage();
    });
    this.$_MSEmitter.on("onShowToastMessageUpdate", (data) => {
      this.contentToastSuccess = data;
      this.onShowToastMessage();
    });
    this.$_MSEmitter.on("setFormModeAdd", () => {
      this.setFormModeAdd();
    });
    this.$_MSEmitter.on("refreshDataTable", async () => {
      await this.getDataTeacher();
    });
    this.$_MSEmitter.on("confirmDeleteEntity", async () => {
      await this.btnConfirmDeleteTeacher();
    });
    this.$_MSEmitter.on("unConfirmDeleteEntity", () => {
      this.btnUnConfirmDeleteTeacher();
    });
    this.$_MSEmitter.on("confirmDeleteMultiple", async () => {
      await this.btnconfirmDeleteMultipleTeacher();
    });
    this.$_MSEmitter.on("closeToastMessage", () => {
      this.btnCloseToastMessage();
    });
    this.$_MSEmitter.on("onSelectedSelectOption", async (item, propCode) => {
      if (propCode == "option_code") {
        await this.handleSelectOptionFilter(item);
      } else if (propCode == "condition_code") {
        await this.handleFilterCondition(item);
      }
    });
    this.$_MSEmitter.on("onSearchChangeSelectOption", async (textSearch, propCode) => {
      if (propCode == "condition_code") {
        await this.onSearchChangeConditionData(textSearch);
      }
    });
  },

  mounted() {
    // Lắng nghe sự kiện click trên toàn bộ màn hình
    window.addEventListener("click", this.handleClickOutsidePaging);
    window.addEventListener("click", this.handleClickOutsideDeleteMulti);
    window.addEventListener("click", this.handleClickOutsideFeature);
  },

  data() {
    return {
      // Khai báo biến quy định trạng thái hiển thị của form chi tiết
      isShowFormDetail: false,
      // Khai báo biến quy định trạng thái hiển thị overlay
      isOverlay: false,
      // Khai báo biến quy định trạng thái hiển thị của các chức năng ở cột cuối của table
      isShowColFeature: false,
      // Khai báo biến quy định trạng thái hiển thị của các item select paging
      isShowPaging: false,
      // Khai báo biến kiểm tra xem form chi tiết đang ở trạng thái thêm hay sửa
      isStatusFormMode: this.$_MSEnum.FORM_MODE.Add,
      // Khai báo trạng thái hiển thị của toast message
      isShowToastMessage: false,
      // Khai báo dữ liệu duyệt trên 1 trang table
      dataTable: [],
      // Khai báo 1 sinh viên được chọn để xử lý hàm sửa
      teacherUpdate: {},
      // Khai báo số bản ghi mặc định được hiển thi trên table
      selectedRecord: this.$_MSEnum.RECORD.RECORD_DEFAULT,
      // Khai báo list số bản ghi có thể lựa chọn để hiển thị trên trang
      recordOptions: this.$_MSEnum.RECORD.RECORD_OPTIONS,
      // Khai báo id của sinh viên cần xóa
      teacherIdDeleteSelected: "",
      // Khai báo teacher_code của sinh viên cần xóa
      teacherCodeDeleteSelected: "",
      // Khai báo biến quy định trạng thái ẩn hiển dialog confirm delete
      isShowDialogConfirmDelete: false,
      // Khai báo biến lưu nội dung của toast message
      contentToastSuccess: "",
      // Tái sử dụng hàm formatDate trong helperCommon
      formatDate: helperCommon.formatDate,
      // Khai báo biến lưu nội dung tìm kiếm
      textSearch: "",
      customFilter: "",
      // Khai báo trang hiện tại trong phân trang
      currentPage: this.$_MSEnum.RECORD.CURRENT_PAGE,
      // Khai báo số trang tối đa hiển thị trong phân trang
      maxVisiblePages: this.$_MSEnum.RECORD.MAX_VISIBLE_PAGE,
      // Khai báo biến quy định trạng thái hiển thị loadding
      isShowLoading: false,
      // Khai báo biến lưu chỉ số index được chọn trong paging
      indexSelectedRecord: this.$_MSEnum.RECORD.INDEX_SELECTED_DEFAULT,
      // Khai báo biến quy định sau 1 khoảng thời gian mới bắt đầu tìm kiếm
      searchTimeout: null,
      // Khai báo biến quy định trạng thái hiển thị của menu thực hiện hàng loạt
      isShowMenuExcuteBatch: false,
      // Khai báo biến lưu danh sách id cần xóa
      ids: [],
      // Khai báo biến kiểm tra xem dialog hiển thị hỏi xóa ít hay xóa nhiều
      isDeleteMultipleDialog: null,
      // Khai báo biến tùy chỉnh top, left cho feature menu
      positionFeatureMenu: {},
      // Khai báo biến lưu teacher khi bấm vào col feature
      selectedTeacher: {},
      // Khai báo biến quy định trạng thái hiển thị tiện ích
      isShowUtilities: false,
      optionFilter: {},
      listOptionFilter: [
        {
          option_code: this.$_MSEnum.FILTER_OPTION.Faculty,
          option_name: "Lọc theo khoa",
        },
        {
          option_code: this.$_MSEnum.FILTER_OPTION.Gender,
          option_name: "Lọc theo giới tính",
        },
        {
          option_code: this.$_MSEnum.FILTER_OPTION.Address,
          option_name: "Lọc theo tỉnh",
        },
      ],
      conditionFilter: {},
      listConditionFilter: [],
      isDisabledMenuConditionFilter: true,
    };
  },

  computed: {
    /**
     * Mô tả: Tính tổng số trang trong phân trang
     * created by : BNTIEN
     * created date: 04-06-2023 02:49:32
     */
    totalPages() {
      return Math.ceil(this.dataTable.TotalRecord / this.selectedRecord);
    },
    /**
     * Mô tả: Nếu đang ở trang đầu thì button prev không hoạt động
     * created by : BNTIEN
     * created date: 27-06-2023 11:19:25
     */
    isFirstPage() {
      return this.currentPage === this.$_MSEnum.RECORD.CURRENT_PAGE;
    },
    /**
     * Mô tả: Nếu đang ở trang cuối thì button next không hoạt động
     * created by : BNTIEN
     * created date: 27-06-2023 11:19:25
     */
    isLastPage() {
      if (!this.totalPages || this.totalPages === 0) {
        return true;
      }
      return this.currentPage === this.totalPages;
    },
    /**
     * Mô tả: Tính tổng số trang sẽ hiển thị
     * created by : BNTIEN
     * created date: 04-06-2023 02:49:32
     */
    visiblePageNumbers() {
      if (!this.dataTable.TotalRecord || this.dataTable.TotalRecord === 0) {
        return [];
      }

      let startPage = Math.max(this.currentPage - Math.floor(this.maxVisiblePages / 2), 1);
      let endPage = startPage + this.maxVisiblePages - 1;
      if (endPage > this.totalPages) {
        endPage = this.totalPages;
        startPage = Math.max(endPage - this.maxVisiblePages + 1, 1);
      }

      const visiblePages = [];
      for (let i = startPage; i <= endPage; i++) {
        visiblePages.push(i);
      }

      return visiblePages;
    },
    /**
     * Mô tả: Kiểm tra xem button thực hiện hàng loạt có ở trạng thái disable hay không
     * created by : BNTIEN
     * created date: 27-06-2023 23:26:41
     */
    isDisableExcuteBatch() {
      return this.ids.length < 2;
    },
    /**
     * Mô tả: Kiểm tra list ids chứa tất cả id trong dataTable hay không
     * created by : BNTIEN
     * created date: 28-06-2023 08:41:29
     */
    isCheckAll() {
      if (!this.dataTable.Data) return false;
      if (this.dataTable.Data.length == 0) return false;
      for (let i = 0; i < this.dataTable.Data.length; i++) {
        if (!this.ids.includes(this.dataTable.Data[i].teacher_id)) {
          return false;
        }
      }
      return true;
    },
  },

  methods: {
    /**
     * Mô tả: Hàm lấy dữ các điều kiện lọc
     * created by : BNTIEN
     * created date: 29-05-2023 07:49:20
     */
    async getDataOptionFilter(optionFilter, textSearch) {
      try {
        this.isShowLoading = true;
        const resfilter = await teacherService.getOptionFilter(optionFilter, textSearch);
        this.isShowLoading = false;
        if (resfilter && resfilter.data && this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(resfilter.data.Code)) {
          this.listConditionFilter = resfilter.data.Data;
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm lấy dữ liệu sinh viên từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:49:20
     */
    async getDataTeacher() {
      try {
        this.isShowLoading = true;
        const resfilter = await teacherService.getFilter(this.selectedRecord, this.currentPage, "", "");
        this.isShowLoading = false;
        this.dataTable = resfilter.data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý sự kiên load lại toàn bộ dữ liệu khi click vào icon refresh
     * created by : BNTIEN
     * created date: 29-05-2023 07:49:31
     */
    async refreshData() {
      this.selectedRecord = this.$_MSEnum.RECORD.RECORD_DEFAULT;
      this.currentPage = this.$_MSEnum.RECORD.CURRENT_PAGE;
      this.indexSelectedRecord = this.$_MSEnum.RECORD.INDEX_SELECTED_DEFAULT;
      this.textSearch = "";
      await this.deleteFilterCondition();
    },
    /**
     * Mô tả: Hàm xử lý sự kiên mở form chi tiết khi click vào button thêm mới sinh viên
     * created by : BNTIEN
     * created date: 29-05-2023 07:48:01
     */
    btnOpenFormDetail() {
      this.isShowFormDetail = true;
      this.isOverlay = true;
      this.teacherUpdate.teacher_code = "";
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi click vào nút close trong form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:48:35
     */
    onCloseFormDetail() {
      this.isShowFormDetail = false;
      this.isOverlay = false;
      this.isStatusFormMode = this.$_MSEnum.FORM_MODE.Add;
      this.teacherUpdate = {};
    },
    /**
     * Mô tả: Hàm xử lý sự kiện đóng mở các menu feature ở cột cuối của table khi click vào icon drop
     * created by : BNTIEN
     * created date: 29-05-2023 07:48:54
     */
    onOpenFeatureMenu(e, teacher) {
      try {
        // chặn sự liện lan ra các phần tử cha
        e.stopPropagation();
        this.selectedTeacher = teacher;
        this.isShowColFeature = true;
        const positionIcon = e.target.getBoundingClientRect();
        const left = positionIcon.right - 110;
        let top = 0;
        if (positionIcon.bottom > 500) {
          top = positionIcon.bottom - 100;
        } else {
          top = positionIcon.bottom + 10;
        }
        this.positionFeatureMenu = { left: left, top: top };
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý sự kiện đóng mở lựa chọn số phần tử hiển thị trên 1 trang trong table
     * created by : BNTIEN
     * created date: 29-05-2023 07:49:05
     */
    onShowSelectPaging() {
      this.isShowPaging = !this.isShowPaging;
    },
    /**
     * Mô tả: Hàm xử lý cập nhật thông tin sinh viên
     * created by : BNTIEN
     * created date: 29-05-2023 07:49:56
     */
    onUpdateFormDetail(teacher) {
      this.teacherUpdate = teacher;
      this.isShowFormDetail = true;
      this.isOverlay = true;
      this.isStatusFormMode = this.$_MSEnum.FORM_MODE.Edit;
    },
    /**
     * Mô tả: Hàm set isStatusFormMode = ADD
     * created by : BNTIEN
     * created date: 03-06-2023 15:37:14
     */
    setFormModeAdd() {
      this.isStatusFormMode = this.$_MSEnum.FORM_MODE.Add;
    },
    /**
     * Mô tả: Hàm xử lý sự kiện click vào các item lựa chọn số bản ghi hiển thị trên table
     * created by : BNTIEN
     * created date: 29-05-2023 07:50:06
     */
    onSelectedRecord(record, index) {
      this.selectedRecord = record;
      this.indexSelectedRecord = index;
      this.currentPage = this.$_MSEnum.RECORD.CURRENT_PAGE;
      this.updateDataTable();
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi bấm vào item xóa sinh viên thì hiển thị dialog xác nhận xóa
     * created by : BNTIEN
     * created date: 29-05-2023 07:50:15
     */
    onDeleteTeacher() {
      this.isShowDialogConfirmDelete = true;
      this.isDeleteMultipleDialog = false;
      this.isOverlay = true;
      this.teacherIdDeleteSelected = this.selectedTeacher.teacher_id;
      this.teacherCodeDeleteSelected = this.selectedTeacher.teacher_code;
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng xác nhận xóa 1 sinh viên
     * created by : BNTIEN
     * created date: 28-05-2023 21:09:01
     */
    async btnConfirmDeleteTeacher() {
      try {
        this.isShowLoading = true;
        const res = await teacherService.delete(this.teacherIdDeleteSelected);
        this.isShowLoading = false;
        this.isShowDialogConfirmDelete = false;
        this.isOverlay = false;
        if (res && res.data && this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) && res.data.Data > 0) {
          this.isDeleteMultipleDialog = false;
          this.contentToastSuccess = this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_DELETE;
          this.onShowToastMessage();
          await this.getDataTeacher();
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi click vào button không trong dialog xác nhận xóa
     * created by : BNTIEN
     * created date: 29-05-2023 07:51:41
     */
    btnUnConfirmDeleteTeacher() {
      this.isShowDialogConfirmDelete = false;
      this.isDeleteMultipleDialog = false;
      this.isOverlay = false;
    },

    /**
     * Mô tả: Hàm xử lý sự kiện mở toast mesage
     * created by : BNTIEN
     * created date: 31-05-2023 00:42:10
     */
    onShowToastMessage() {
      this.isShowToastMessage = true;
      setTimeout(() => {
        this.isShowToastMessage = false;
      }, 3000);
    },

    /**
     * Mô tả: Hàm xử lý sự kiện đóng toast mesage
     * created by : BNTIEN
     * created date: 31-05-2023 00:42:10
     */
    btnCloseToastMessage() {
      this.isShowToastMessage = false;
    },
    /**
     * Mô tả: Hàm nhân bản 1 sinh viên
     * created by : BNTIEN
     * created date: 28-06-2023 13:59:30
     */
    onDupliCateTeacher() {
      try {
        this.teacherUpdate = this.selectedTeacher;
        this.isShowFormDetail = true;
        this.isOverlay = true;
        this.isStatusFormMode = this.$_MSEnum.FORM_MODE.Add;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm tìm kiếm sinh viên theo mã hoặc tên
     * created by : BNTIEN
     * created date: 04-06-2023 00:20:21
     */
    async onSearchTeacher() {
      try {
        this.currentPage = this.$_MSEnum.RECORD.CURRENT_PAGE;
        if (!this.textSearch.trim()) {
          this.textSearch = "";
        }
        if (!this.customFilter.trim()) {
          this.customFilter = "";
        }
        this.isShowLoading = true;
        const filteredTeachers = await teacherService.getFilter(
          this.selectedRecord,
          this.currentPage,
          this.textSearch.trim(),
          this.customFilter.trim()
        );
        this.isShowLoading = false;
        this.dataTable = filteredTeachers.data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Tự động tìm kiếm sau 1 khoảng thời gian người dùng không nhập dữ liệu
     * created by : BNTIEN
     * created date: 27-06-2023 11:44:30
     */
    async autoSearch() {
      try {
        clearTimeout(this.searchTimeout);
        this.searchTimeout = setTimeout(async () => {
          await this.onSearchTeacher();
        }, 500);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Cập nhật danh sách dữ liệu hiển thị dựa trên currentPage và pageSize
     * created by : BNTIEN
     * created date: 04-06-2023 01:49:06
     */
    async updateDataTable() {
      try {
        if (!this.textSearch.trim()) {
          this.textSearch = "";
        }
        if (!this.customFilter.trim()) {
          this.customFilter = "";
        }
        this.isShowLoading = true;
        const resfilter = await teacherService.getFilter(
          this.selectedRecord,
          this.currentPage,
          this.textSearch.trim(),
          this.customFilter.trim()
        );
        this.isShowLoading = false;
        this.dataTable = resfilter.data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Di chuyển giữa các trang trong phân trang
     * created by : BNTIEN
     * created date: 04-06-2023 01:49:32
     */
    async goToPage(p) {
      let newPage;
      if (p === this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PAGE.PREVIOUS && this.currentPage > 1) {
        newPage = this.currentPage - 1;
      } else if (
        p === this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PAGE.NEXT &&
        this.currentPage < this.totalPages
      ) {
        newPage = this.currentPage + 1;
      } else if (
        typeof p === this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.PAGE.NUMBER &&
        p >= 1 &&
        p <= this.totalPages
      ) {
        newPage = p;
      }

      if (newPage !== undefined && newPage !== this.currentPage) {
        this.currentPage = newPage;
        await this.updateDataTable();
      }
    },
    /**
     * Mô tả: xử lý sự kiện khi người dùng click ra ngoài select paging
     * created by : BNTIEN
     * created date: 08-06-2023 04:50:25
     */
    handleClickOutsidePaging(event) {
      if (!this.$refs.PagingMenu.contains(event.target)) {
        this.isShowPaging = false;
      }
    },

    /**
     * Mô tả: xử lý sự kiện click ra ngoài menu thực hiện hàng loạt
     * created by : BNTIEN
     * created date: 30-06-2023 21:53:38
     */
    handleClickOutsideDeleteMulti(event) {
      if (!this.$refs.DeleteMulti.contains(event.target)) {
        this.isShowMenuExcuteBatch = false;
      }
    },

    /**
     * Mô tả: xử lý sự kiện click ngoài menu feature
     * created by : BNTIEN
     * created date: 03-07-2023 00:03:06
     */
    handleClickOutsideFeature() {
      this.isShowColFeature = false;
    },

    /**
     * Mô tả: Hàm ẩn hiện menu thực hiện hàng loạt
     * created by : BNTIEN
     * created date: 27-06-2023 23:37:04
     */
    onShowExcuteBatch() {
      this.isShowMenuExcuteBatch = !this.isShowMenuExcuteBatch;
    },

    /**
     * Mô tả: Hàm kiểm tra xem checkbox từng hàng đã chọn chưa, nếu rồi thì bỏ chọn và ngược lại
     * created by : BNTIEN
     * created date: 28-06-2023 09:30:13
     */
    checkRow(id) {
      try {
        if (!id) return this.ids;
        if (this.ids.includes(id)) {
          this.ids.splice(this.ids.indexOf(id), 1);
          return this.ids;
        }
        this.ids.push(id);
        return this.ids;
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Xử lý hàm chọn tất cả ở ô checkbox thead
     * created by : BNTIEN
     * created date: 28-06-2023 09:31:07
     */
    checkAllSelect() {
      try {
        if (this.isCheckAll) {
          this.dataTable.Data.map((item) => {
            if (this.ids.includes(item.teacher_id)) {
              this.ids.splice(this.ids.indexOf(item.teacher_id), 1);
            }
          });
        } else {
          if (this.dataTable.Data) {
            this.dataTable.Data.map((item) => {
              if (!this.ids.includes(item.teacher_id)) {
                this.ids.push(item.teacher_id);
              }
            });
          }
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hiển thị form xác nhận xóa nhiều
     * created by : BNTIEN
     * created date: 28-06-2023 10:34:10
     */
    onShowDialogDeleteMulti() {
      this.isShowDialogConfirmDelete = !this.isShowDialogConfirmDelete;
      this.isOverlay = true;
      this.isDeleteMultipleDialog = true;
    },
    /**
     * Mô tả: Hàm thực hiện xóa nhiều sinh viên theo list id đã chọn
     * created by : BNTIEN
     * created date: 28-06-2023 09:36:08
     */
    async btnconfirmDeleteMultipleTeacher() {
      try {
        this.isShowLoading = true;
        const res = await teacherService.deleteMutiple(this.ids);
        this.isShowLoading = false;
        this.isShowDialogConfirmDelete = false;
        this.isOverlay = false;
        if (res && res.data && this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) && res.data.Data) {
          this.ids = [];
          this.isDeleteMultipleDialog = false;
          this.contentToastSuccess = this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_DELETE;
          this.onShowToastMessage();
          await this.getDataTeacher();
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Xử lý xuất dữ liệu ra excel
     * created by : BNTIEN
     * created date: 01-07-2023 22:35:32
     */
    async exportExcel() {
      try {
        const link = this.$refs.ExportTeacher;
        this.isShowLoading = true;
        await teacherService.exportData(
          link,
          this.dataTable.TotalRecord,
          1,
          this.textSearch.trim(),
          this.customFilter.trim()
        );
        this.isShowLoading = false;
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Xử lý chọn điều kiện lọc
     * created by : BNTIEN
     * created date: 18-04-2024 20:53:30
     */
    async handleSelectOptionFilter(item) {
      this.optionFilter = item;
      this.isDisabledMenuConditionFilter = false;
      await this.getDataOptionFilter(item.option_code, "");
    },

    /**
     * Mô tả: Xử lý lọc dữ liệu theo điều kiện đã chọn
     * created by : BNTIEN
     * created date: 18-04-2024 21:19:13
     */
    async handleFilterCondition(item) {
      this.conditionFilter = item;
      switch (this.optionFilter.option_code) {
        case this.$_MSEnum.FILTER_OPTION.Faculty:
          this.customFilter = `f.faculty_code ilike '%${item.condition_code}%'`;
          break;
        case this.$_MSEnum.FILTER_OPTION.Gender:
          this.customFilter = `tc.gender ilike '%${item.condition_code}%'`;
          break;
        case this.$_MSEnum.FILTER_OPTION.Address:
          this.customFilter = `tc.address ilike '%${item.condition_name}%'`;
          break;
        default:
          break;
      }
      await this.onSearchTeacher();
    },

    /**
     * Mô tả: Xóa điều kiện lọc
     * created by : BNTIEN
     * created date: 18-04-2024 21:11:49
     */
    async deleteFilterCondition() {
      this.optionFilter = {};
      this.conditionFilter = {};
      this.customFilter = "";
      this.isDisabledMenuConditionFilter = true;
      this.onSearchTeacher();
    },

    /**
     * Mô tả: Tìm kiếm ở select option chọn điều kiện lọc
     * created by : BNTIEN
     * created date: 27-04-2024 14:20:28
     */
    async onSearchChangeConditionData(textSearch) {
      try {
        this.conditionFilter.condition_name = textSearch;
        await this.getDataOptionFilter(this.optionFilter.option_code, textSearch);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Xử lý nhập điểm từ file excel
     * created by : BNTIEN
     * created date: 27-02-2024 21:11:46
     */
    async importExcel($event) {
      const file = $event.target.files[0];
      const formData = new FormData();
      formData.append("file", file);
      try {
        const res = await teacherService.importExcel(formData);
        if (res && res.data && res.data.Code == this.$_MSEnum.STATUS.OK) {
          this.contentToastSuccess = "Nhập khẩu thành công";
          this.onShowToastMessage();
          await this.getDataTeacher();
        }
      } catch (error) {
        this.contentToastSuccess = "Nhập khẩu không thành thành công";
        this.onShowToastMessage();
        return;
      }
    },
  },

  beforeUnmount() {
    // Hủy các sự kiện đã đăng kí
    this.$_MSEmitter.off("onShowToastMessage");
    this.$_MSEmitter.off("onShowToastMessageUpdate");
    this.$_MSEmitter.off("setFormModeAdd");
    this.$_MSEmitter.off("refreshDataTable");
    this.$_MSEmitter.off("confirmDeleteEntity");
    this.$_MSEmitter.off("unConfirmDeleteEntity");
    this.$_MSEmitter.off("confirmDeleteMultiple");
    this.$_MSEmitter.off("closeToastMessage");
    this.$_MSEmitter.off("onSelectedSelectOption");
    this.$_MSEmitter.off("onSearchChangeSelectOption");
    window.removeEventListener("click", this.handleClickOutsidePaging);
    window.removeEventListener("click", this.handleClickOutsideDeleteMulti);
    window.removeEventListener("click", this.handleClickOutsideFeature);
  },
};
</script>

<style scoped>
@import url(@/css/maincontent.css);
@import url(@/css/paging.css);

.rotate-function-icon {
  transform: rotate(180deg);
}

.active-page {
  border: 1px solid var(--color-border-default);
  font-weight: 700;
}

.active-record {
  border: 1px solid var(--color-btn-default);
}

input[type="checkbox"] {
  accent-color: #2ca01c;
  width: 16px;
  height: 16px;
  cursor: pointer;
}

.active-record-item {
  background-color: var(--color-btn-default);
  color: white;
}

.no-disable {
  border: 1px solid black;
}

.no-disable:hover {
  background-color: #e0e0e0;
  cursor: pointer;
}

.checkedRow {
  background-color: #f9ecca;
}

.checkedRow td:first-child,
.checkedRow td:last-child {
  background-color: #f9ecca;
}

.loadding-form-detail {
  left: 50%;
}

.no-data {
  position: fixed;
  top: 50%;
  left: 50%;
}
</style>
