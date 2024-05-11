<template>
  <div
    class="e-cbb"
    id="e-cbb"
    :class="{
      'border-red': isBorderRedCBB[propName] || isBorderRedCBB[propId],
    }"
  >
    <div class="e-textfield-cbb">
      <div class="container-input">
        <ms-input
          ref="InputCBB"
          :placeholder="placeholderInputCBB"
          :value="entityCBB[propName]"
          @input="onSearchChange"
          :tabindex="tabindexCBB"
          @keydown="onKeyDownEntity"
          @mouseenter="isHoveringCBB = true"
          @mouseleave="isHoveringCBB = false"
        ></ms-input>
        <div
          class="ms-tooltip"
          v-if="
            isHoveringCBB &&
            (isBorderRedCBB[propName] || isBorderRedCBB[propId] || !entityCBB[propName]) &&
            (errorsCBB[propName] || errorsCBB[propId])
          "
        >
          {{ errorsCBB[propName] ? errorsCBB[propName] : errorsCBB[propId] }}
        </div>
      </div>
    </div>
    <div class="e-icon-cbb" @click="onShowSelectEntity">
      <div class="function-icon"></div>
    </div>
  </div>
  <div class="col-md-l select-entity" :class="{ 'select-entity-block': isShowSelectEntity }" ref="MenuCBB">
    <ul v-show="isShowSelectEntity" @click="onShowSelectEntity">
      <li
        v-for="(item, index) in listEntitySearchCBB"
        :key="index"
        @click="onSelectedEntity(item, index)"
        :class="{ 'cbb-selected': index == indexEntitySelected }"
        ref="EntitySelectedItem"
      >
        {{ item[propName] }}
      </li>
      <li v-if="!listEntitySearchCBB.length" :class="{ 'not-found-entity': !listEntitySearchCBB.length }">
        {{ this.$_MSResource[this.$_LANG_CODE].FORM.NOT_FOUND }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "ComboboxDefault",
  props: [
    "propName",
    "propId",
    "isBorderRedCBB",
    "entityCBB",
    "errorsCBB",
    "tabindexCBB",
    "listEntitySearchCBB",
    "placeholderInputCBB",
    "indexSelectedCBB",
  ],
  data() {
    return {
      // Khai báo biến kiểm tra xem ô input có đang được hover hay không
      isHoveringCBB: false,
      // Khai báo biến quy định trạng thái hiển thị của combobox chọn đơn vị
      isShowSelectEntity: false,
      // Khai báo chỉ số đang được chọn trong combobox
      indexEntitySelected: 0,
    };
  },

  mounted() {
    this.$_MSEmitter.on("focusInputCBB", () => {
      this.$refs["InputCBB"].focus();
    });
    if (this.indexSelectedCBB) {
      this.indexEntitySelected = this.indexSelectedCBB;
    }
    window.addEventListener("mousedown", this.closeMenuCBB);
  },

  methods: {
    focusCombobox() {
      let me = this;
      me.$refs["InputCBB"].focus();
    },
    /**
     * Mô tả: Toggle menu
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:42
     */
    onShowSelectEntity() {
      this.isShowSelectEntity = !this.isShowSelectEntity;
    },

    /**
     * Mô tả: Chọn item trong combobox
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedEntity(item, index) {
      this.$_MSEmitter.emit("onSelectedEntityCBB", item, this.propName);
      this.indexEntitySelected = index;
    },

    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChange() {
      try {
        await this.$_MSEmitter.emit("onSearchChangeCBB", event.target.value, this.propName);
        this.isShowSelectEntity = true;
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
     * Mô tả: Xử lý keydown lên, xuống, enter trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:02:18
     */
    onKeyDownEntity(event) {
      try {
        const maxLength = this.listEntitySearchCBB.length;
        if (maxLength == 0) {
          return;
        } else {
          if (event.keyCode == this.$_MSEnum.KEY_CODE.DOWN) {
            // Bấm xuống
            if (this.isShowSelectEntity) {
              if (this.indexEntitySelected < maxLength - 1) {
                this.indexEntitySelected++;
              } else if (this.indexEntitySelected == maxLength - 1) {
                this.indexEntitySelected = 0;
              }
              // scroll focus theo item được chọn
              this.scrollIndex(this.indexEntitySelected, this.$_MSEnum.KEY_CODE.DOWN);
            } else {
              this.isShowSelectEntity = true;
            }
          } else if (event.keyCode == this.$_MSEnum.KEY_CODE.UP) {
            // Bấm lên
            if (this.isShowSelectEntity) {
              if (this.indexEntitySelected > 0) {
                this.indexEntitySelected--;
              } else if (this.indexEntitySelected == 0) {
                this.indexEntitySelected = maxLength - 1;
              }
              // scroll focus theo item được chọn
              this.scrollIndex(this.indexEntitySelected, this.$_MSEnum.KEY_CODE.UP);
            } else {
              this.isShowSelectEntity = true;
            }
          } else if (event.keyCode == this.$_MSEnum.KEY_CODE.ENTER) {
            // Bấm enter
            if (this.isShowSelectEntity) {
              this.$_MSEmitter.emit("onKeyDownEntityCBB", this.indexEntitySelected, this.propName);
              this.isShowSelectEntity = false;
            } else {
              this.isShowSelectEntity = true;
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
      if (this.isShowSelectEntity && !this.$refs.MenuCBB.contains(event.target)) {
        this.isShowSelectEntity = false;
      }
    },
  },

  beforeUnmount() {
    this.$_MSEmitter.off("focusInputCBB");
    window.removeEventListener("mousedown", this.closeMenuCBB);
  },
};
</script>

<style scoped>
@import url(./ComboboxDefault.css);

.select-entity-block {
  background-color: #fff;
  border: 1px solid var(--color-border-default);
  z-index: 100;
}

.e-textfield-cbb .textfield {
  border-radius: 4px 0px 0px 4px;
}

.cbb-selected {
  background-color: var(--color-border-default);
  color: var(--color-btn-default);
}

.not-found-entity {
  text-align: center;
  opacity: 0.5;
}

.border-red {
  border: 1px solid red;
}

#detail-add-system-account .cbb-selected {
  background-color: var(--color-btn-default);
  color: #ffffff;
}
</style>
