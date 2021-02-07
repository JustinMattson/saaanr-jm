<template>
  <li type="button" @click="save">
    <i class="fas fa-long-arrow-alt-right fa-sm text-warning"></i>
    {{vault.name}}
  </li>
</template>


<script>
export default {
  name: "VKsaveComponent",
  props: ["keep", "vault"],
  computed: {},
  methods: {
    save() {
      this.$store.dispatch("createVK", {
        keepId: this.keep.id,
        vaultId: this.vault.id
      });
    }
  },
  components: {}
};
</script>


<style scoped>
</style>