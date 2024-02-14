<template>
  <textarea
    class="textfield-area"
    type="text"
    :value="modelValue"
    @input="$emit('update:modelValue', $event.target.value)"
    :title="titleContent"
    ref="inputTextArea"
  ></textarea>
</template>

<script>
export default {
  name: "InputTextArea",
  props: {
    modelValue: String,
    titleContent: String,
  },
  methods: {
    focus() {
      this.$refs.inputTextArea.focus();
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/textarea.css);

textarea {
  resize: none;
}
</style>
