<template>
  <div class="vaultdetails container-fluid">
    <div class="row">
      <div class="col-12">
        <!-- TODO insert means to edit Vault info -->
        <h1>{{vault.name}}</h1>
        <!-- Vault Name: {{vault.name}}
        <br />-->
        <span class="text-muted">Vault Description:</span>
        {{vault.description}}
        <br />
        <i class="fas fa-pencil-alt text-info action" @click="toggleEdit"></i>
        <small>&nbsp;Edit Vault</small>
        <p />

        <!-- EDIT VAULT FORM -->
        <form class="form text-muted" @submit.prevent="editVault" v-show="editDetails">
          Vault Name:
          <input type="text" v-model="vault.name" style="width:50vw;" />
          <br />Vault Description:
          <input type="text" v-model="vault.description" style="width:50vw;" />
          <br />

          <i
            type="submit"
            class="far fa-save text-info fa-2x action shadowtext-shadow"
            title="Save Changes"
            @click="updateVault"
          ></i>
        </form>
        <!-- END EDIT VAULT FORM -->
        <small
          class="text-muted"
          v-show="keepsByVault.length < 1"
        >Keeps stored in this vault will be displayed here</small>
      </div>
      <!-- Display keeps within this vault -->
      <!-- This will be from the vaultKeeps table -->
      <div class="col-12 list-container d-flex justify-content-center">
        <div id="keeps" class="card-columns p-2" style="column-gap: 1rem;">
          <!-- KEEP COMPONENTS BEGIN -->
          <keep v-for="keep in keepsByVault" :key="keep.id" :keep="keep" />
          <!-- KEEP COMPONENTS END -->
        </div>
      </div>
      <div class="col-12">
        <i class="far fa-trash-alt text-danger action" @click="deleteVault"></i>
        <small>&nbsp;Delete Vault</small>
        <br />
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "@/components/KeepComponent.vue";
import swal from "sweetalert";
export default {
  name: "vaultdetails",
  data() {
    return {
      editDetails: false,
    };
  },
  async mounted() {
    await this.$store.dispatch("getVaultById", this.$route.params.vaultId);
    await this.$store.dispatch("getKeepsByVaultId", this.$route.params.vaultId);
    // below moved to store/index.js
    // await this.$store.dispatch("getUserVKs");
    // await this.$store.dispatch("getKeeps");
    // await this.$store.dispatch("getUserVaults");
  },
  beforeRouteLeave(to, from, next) {
    if (to.name != "vaultdetails") {
      this.$store.commit("setActiveVault", {});
      this.$store.commit("setKeepsByVault", []);
    }
    next();
  },
  computed: {
    vault() {
      return this.$store.state.activeVault;
    },
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    keepsByVault() {
      return this.$store.state.keepsByVault;
    },
  },
  methods: {
    updateVault() {
      this.$store.dispatch("editVault", this.vault);
      this.editDetails = false;
    },
    toggleEdit() {
      this.editDetails = !this.editDetails;
    },
    deleteVault() {
      swal({
        title: "Are you sure?",
        text:
          "Click 'Ok' to confirm you wish to delete this Vault.  This action cannot be undone.",
        icon: "error",
        buttons: true,
        dangerMode: true,
      }).then((removeVault) => {
        if (removeVault) {
          let data = this.$store.dispatch(
            "deleteVault",
            this.$route.params.vaultId
          );
          swal("Poof! Vault has been deleted!", {
            icon: "success",
          });
        } else {
          swal("Delete cancelled");
        }
      });
    },
  },
  components: {
    Keep,
  },
};
</script>

<style scoped></style>
