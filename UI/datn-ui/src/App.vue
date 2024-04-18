<template>
  <div>
    <div class="container" v-if="hasToken">
      <!-- sidebar -->
      <TheSidebar></TheSidebar>
      <div id="content" class="content">
        <!-- header -->
        <TheHeader></TheHeader>
        <!-- main -->
        <TheMain></TheMain>
      </div>
    </div>
    <div v-else>
      <LoginPage></LoginPage>
    </div>
  </div>
</template>

<script>
import TheSidebar from "./layout/TheSidebar.vue";
import TheHeader from "./layout/TheHeader.vue";
import TheMain from "./layout/TheMain.vue";
import LoginPage from "./views/login_app/LoginPage.vue";

export default {
  name: "App",
  components: {
    TheSidebar,
    TheHeader,
    TheMain,
    LoginPage,
  },
  data() {
    return {
      hasToken: false,
    };
  },
  mounted() {
    this.checkToken();
  },
  methods: {
    checkToken() {
      if (sessionStorage.getItem("token")) {
        this.hasToken = true;
      } else {
        this.hasToken = false;
      }
    },
  },
};
</script>

<style>
@import url(@/css/blockexercise.css);
@import url(@/css/base/icon.css);
@import url(@/css/varialble.css);
</style>
