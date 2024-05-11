<template>
  <div class="cbb-select-multiple">
    <div class="cbb-select-multiple-main">
      <template v-for="(item, index) in listDataSelected" :key="index">
        <div class="item-select-multiple" v-if="listDataSelected.length > 0">
          <div class="item-select-multiple-text">{{ item[propCode] }}</div>
          <div class="item-select-multiple-icon" @click="deleteItem(item)">
            <div class="close-small-icon icon-n"></div>
          </div>
        </div>
      </template>
      <div class="textfiled-cbb-select-multiple">
        <input
          class="input-cbb-select-multiple"
          type="text"
          v-model="textSearch"
          @input="onSearchChange"
          @keydown="onKeyDownEntity"
        />
      </div>
    </div>
    <div class="cbb-select-multiple-action">
      <div class="select-multiple-action-add">
        <div class="plus-green-icon"></div>
      </div>
      <div class="select-multiple-action-dropdown" @click="this.isShowSelectMenu = !this.isShowSelectMenu">
        <div class="function-icon"></div>
      </div>
    </div>
  </div>
  <div class="select-item-cbb-multiple" style="position: relative" v-if="isShowSelectMenu" ref="MenuCBB">
    <div class="form-cbb-menu" id="form-cbb-menu-select-multiple">
      <div class="form-cbb-menu-title">
        <div class="col-1-cbb-menu-item">Mã nhóm KH</div>
        <div class="col-2-cbb-menu-item">Tên nhóm KH, NCC</div>
      </div>
      <div class="form-cbb-main" ref="cbbSelectMultiple" @scroll="handleScroll">
        <template v-for="(item, index) in listEntitySearchCBB.Data" :key="item[propId]">
          <div
            class="form-cbb-menu-item"
            @click="toggleSelectEntity(item)"
            v-if="listEntitySearchCBB.Data.length > 0"
            ref="EntitySelectedItem"
            :class="{ 'cbb-selected': index == indexEntitySelected }"
          >
            <div class="col-1-cbb-menu-item">{{ item[propCode] }}</div>
            <div class="col-2-cbb-menu-item">
              {{ item[propName] }}
            </div>
            <div class="checked-green-icon" v-show="listCode.includes(item[propCode])"></div>
          </div>
        </template>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ComboboxMultiple",

  props: ["listDataSelected", "propId", "propCode", "propName", "listEntitySearchCBB"],

  computed: {
    /**
     * Mô tả: Lấy danh sách mã nhóm nhà cung cấp được chọn
     * created by : BNTIEN
     * created date: 16-08-2023 11:14:06
     */
    listCode() {
      if (this.listDataSelected) {
        return this.listDataSelected.map((x) => x[this.propCode]);
      }
      return [];
    },
  },

  mounted() {
    window.addEventListener("mousedown", this.closeMenuCBB);
  },

  data() {
    return {
      // Biến quy định trạng thái hiển thị select menu
      isShowSelectMenu: false,
      // Khai báo biến lưu giá trị tìm kiếm group
      textSearch: "",
      // Khai báo chỉ số index đang scroll
      indexEntitySelected: 0,
    };
  },

  methods: {
    /**
     * Mô tả: Toggle menu
     * created by : BNTIEN
     * created date: 29-07-2023 21:35:19
     */
    toggleSelectEntity(item) {
      try {
        const isUnSelect = this.listCode.includes(item[this.propCode]);
        this.$_MSEmitter.emit("toggleCBBSelectMultiple", item, isUnSelect);
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Xóa item khi click icon x
     * created by : BNTIEN
     * created date: 29-07-2023 22:36:53
     */
    deleteItem(item) {
      try {
        this.$_MSEmitter.emit("deleteItemCBBSelectMultiple", item);
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Hàm để xử lý khi scroll combobox
     * created by : BNTIEN
     * created date: 29-07-2023 22:45:32
     */
    async handleScroll() {
      try {
        const cbb = this.$refs.cbbSelectMultiple;
        const remainingSpace = cbb.scrollHeight - (cbb.scrollTop + cbb.clientHeight);

        // Kiểm tra xem đã scroll đến cuối combobox chưa
        if (remainingSpace <= 10) {
          // Gọi API để lấy thêm dữ liệu
          await this.$_MSEmitter.emit("handleScrollCBBSelectMultiple", this.textSearch);
        }
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 29-07-2023 23:18:53
     */
    async onSearchChange() {
      try {
        await this.$_MSEmitter.emit("onSearchChangeCBBSelectMultiple", this.textSearch);
        this.isShowSelectMenu = true;
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Hàm xử scroll theo khi bấm lên xuống
     * created by : BNTIEN
     * created date: 07-06-2023 08:37:33
     */
    scrollIndex(index, checkKeyCode) {
      try {
        const element = this.$refs.EntitySelectedItem[index];
        if (checkKeyCode === this.$_MSEnum.KEY_CODE.DOWN) {
          element.scrollIntoView({
            block: this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SCROLL.END,
          });
        } else if (checkKeyCode === this.$_MSEnum.KEY_CODE.UP) {
          element.scrollIntoView({
            block: this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SCROLL.START,
          });
        }
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Xử kí keydown lên xuống, enter
     * created by : BNTIEN
     * created date: 06-06-2023 22:02:18
     */
    onKeyDownEntity(event) {
      try {
        const maxLength = this.listEntitySearchCBB.Data.length;
        if (maxLength == 0) {
          return;
        } else {
          if (event.keyCode == this.$_MSEnum.KEY_CODE.DOWN) {
            // Bấm xuống
            if (this.isShowSelectMenu) {
              if (this.indexEntitySelected < maxLength - 1) {
                this.indexEntitySelected++;
              } else if (this.indexEntitySelected == maxLength - 1) {
                this.indexEntitySelected = 0;
              }
              // scroll focus theo item được chọn
              this.scrollIndex(this.indexEntitySelected, this.$_MSEnum.KEY_CODE.DOWN);
            } else {
              this.isShowSelectMenu = true;
              this.indexEntitySelected = 0;
            }
          } else if (event.keyCode == this.$_MSEnum.KEY_CODE.UP) {
            // Bấm lên
            if (this.isShowSelectMenu) {
              if (this.indexEntitySelected > 0) {
                this.indexEntitySelected--;
              } else if (this.indexEntitySelected == 0) {
                this.indexEntitySelected = maxLength - 1;
              }
              // scroll focus theo item được chọn
              this.scrollIndex(this.indexEntitySelected, this.$_MSEnum.KEY_CODE.UP);
            } else {
              this.isShowSelectMenu = true;
              this.indexEntitySelected = 0;
            }
          } else if (event.keyCode == this.$_MSEnum.KEY_CODE.ENTER) {
            // Bấm enter
            if (this.isShowSelectMenu) {
              this.toggleSelectEntity(this.listEntitySearchCBB.Data[this.indexEntitySelected]);
            } else {
              this.isShowSelectMenu = true;
              this.indexEntitySelected = 0;
            }
          } else if (event.keyCode == this.$_MSEnum.KEY_CODE.BACK_SPACE) {
            if (!this.textSearch) {
              this.deleteItem(this.listDataSelected[this.listDataSelected.length - 1]);
            }
          }
        }
      } catch {
        return;
      }
    },

    /**
     * Mô tả: xử lý click outside
     * created by : BNTIEN
     * created date: 16-08-2023 09:48:35
     */
    closeMenuCBB() {
      if (this.isShowSelectMenu && !this.$refs.MenuCBB.contains(event.target)) {
        this.isShowSelectMenu = false;
      }
    },
  },

  beforeUnmount() {
    window.removeEventListener("mousedown", this.closeMenuCBB);
  },
};
</script>

<style scoped>
@import url(./ComboboxSelectMultiple.css);

.cbb-selected {
  background-color: var(--color-border-default);
  color: var(--color-btn-default);
}
</style>
