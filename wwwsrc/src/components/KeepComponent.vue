<template>
  <div class="keep container p-0">
    <!-- KEEP TEMPLATE -->
    <!-- TODO Router Link text-decoration:none -->
    <router-link :to="{ name: 'keepdetails', params: { keepId: keep.id } }" title="keep, clickable">
      <div class="card text-shadow border-info mb-3" style="max-width: 18rem;">
        <div class="card-body p-0">
          <span class="d-flex justify-content-between card-header border-info">
            <span class="card-title" title="keeps">Keeps: {{ keep.keeps }}</span>
            <span class="card-title" title="number of views">Views: {{ keep.views }}</span>
          </span>
          <img class="card-img" :src="keep.img" alt=""/>
          <div class="p-2" title="keep title">{{ keep.name }}</div>
          <div class="p-2" title="keep description">{{ keep.description }}</div>
        </div>
        <div class="card-footer border-info">
          <span class="d-flex justify-content-between">
            <i
              class="far fa-trash-alt text-danger action"
              v-show="keep.isPrivate == true"
              title="Delete Keep"
              @click.stop.prevent="deleteKeep"
            ></i>
            <i
              class="fas fa-eraser text-warning action"
              v-show="keep.vaultKeepId != null"
              title="Remove Keep from Vault"
              @click.stop.prevent="deleteVaultKeep"
            ></i>
            <i
              class="fas fa-lock-open action"
              title="Publish Keep"
              v-show="keep.isPrivate == true"
              @click.stop.prevent="publish"
            ></i>

            <small class="card-text text-muted">kId: {{ keep.id }}</small>
            <i
              class="fas fa-heart text-danger action"
              title="Add to Vault"
              v-show="!keep.isPrivate"
            ></i>
          </span>
        </div>
      </div>
    </router-link>
    <!-- END KEEP TEMPLATE -->
  </div>
</template>

<script>
import swal from "sweetalert";
export default {
  name: "keep",
  props: ["keep"],
  data() {
    return {};
  },
  mounted() {},
  computed: {},
  methods: {
    // addKeepToVault() {
    //   // this.$store.dispatch("createVaultKeep") // MOVED to KeepDetails
    // },
    deleteVaultKeep() {
      swal({
        title: "Are you sure?",
        text:
          "Click 'Ok' to confirm you wish to remove this Keep from this Vault.  This action cannot be undone.",
        icon: "error",
        buttons: true,
        dangerMode: true,
      }).then((removeKeep) => {
        if (removeKeep) {
          this.keep.vaultId = this.$route.params.vaultId;
          let data = this.$store.dispatch("deleteVK", this.keep);
          swal("Poof! Keep has been removed from Vault!", {
            icon: "success",
          });
        } else {
          swal("Removing Keep from Vault cancelled.");
        }
      });
    },
    publish() {
      swal({
        title: "Are you sure you want to make this Keep public?",
        text:
          "Click 'Ok' to confirm you wish to publish this Keep.  This action cannot be undone.",
        icon: "warning",
        buttons: true,
        dangerMode: true,
      }).then((update) => {
        if (update) {
          this.keep.isPrivate = false;
          let data = this.$store.dispatch("editKeep", this.keep);
          swal("Poof! Your Keep has been published!", {
            icon: "success",
          });
          // this.editKeep = false;
        } else {
          swal("Publish cancelled");
        }
      });
    },
    deleteKeep() {
      swal({
        title: "Are you sure?",
        text:
          "Click 'Ok' to confirm you wish to delete this Keep.  This action cannot be undone.",
        icon: "error",
        buttons: true,
        dangerMode: true,
      }).then((deleteMe) => {
        if (deleteMe) {
          let data = this.$store.dispatch("deleteKeep", this.keep.id);
          swal("Poof! Your Keep has been removed!", {
            icon: "success",
          });
          // this.editKeep = false;
        } else {
          swal("Delete cancelled");
        }
      });
    },
  },
  components: {},
};
</script>

<style scoped></style>
