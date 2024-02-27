<template>
  <button @click="isShowImportExcel = !isShowImportExcel" :class="{ bcg: isShowImportExcel }">
    <div class="dropdown-white-icon">
      <!-- <div class="import-excel" v-if="isShowImportExcel" @click="importEx">
        {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.IMPORT_EXCEL }}
      </div> -->
      <input class="import-excel" v-if="isShowImportExcel" type="file" @change="importEx($event)" />
    </div>
  </button>
</template>

<script>
export default {
  name: "ButtonIcon",

  data() {
    return {
      // Khai báo biến quy định trạng thái hiển thị nhập excel
      isShowImportExcel: false,
    };
  },

  methods: {
    // Xử lí xuất excel
    importEx($event) {
      this.$emit("importExcel", $event);
    },
  },
};
</script>

<style scoped>
@import url(./ButtonIcon.css);

.bcg {
  outline: 2px solid var(--color-border-default);
}
</style>
