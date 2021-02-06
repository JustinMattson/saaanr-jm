<template>
  <div id="app">
    <navbar />
    <router-view />
  </div>
</template>

<script>
import Navbar from "@/components/navbar";
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "App",
  async beforeCreate() {
    await onAuth();
    this.$store.dispatch("setBearer", this.$auth.bearer);
  },
  components: {
    Navbar,
  },
};
</script>

<style lang="scss">
@import "./assets/_variables.scss";
@import "bootstrap";
@import "./assets/_overrides.scss";
// @import url("https://fonts.googleapis.com/css2?family=Lacquer&display=swap");
@import url("https://fonts.googleapis.com/css2?family=Lobster&display=swap");

* {
  // font-family: "Lacquer", cursive;
  font-family: "Lobster", cursive;
}
.text-shadow {
  text-shadow: 1px 1px black;
}
.action {
  cursor: pointer;
}
</style>
