<template>
  <div class="keepdetails container-fluid">
    <h1 class="text-center">Keep Details Vue</h1>
    <div class="row d-flex justify-content-center">
      <div class="col-12 col-md-6 py-3">
        <img class="card-img" :src="keep.img" />
      </div>
      <!-- TODO insert means to edit Keep info -->
      <div class="col-12 col-md-6 py-3">
        <small class="text-info">Name:</small>
        {{keep.name}}
        <br />
        <small class="text-info">Description:</small>
        {{keep.description}}
        <br />
        <small class="text-info">Private:</small>
        {{keep.isPrivate}}
        <br />
        <small class="text-info">Views:</small>
        {{keep.views}}
        <br />
        <p>
          <small class="text-info">Keeps:</small>
          {{keep.keeps}}
        </p>

        <div class="col-12 d-flex justify-content-around"></div>
        <!-- VAULT MODAL FORM -->
        <div class="modal fade" id="vaultModal" role="dialog">
          <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
              <div class="modal-header bg-primary shadow-sm">
                <h4 class="modal-title text-white">New Vault</h4>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
              </div>
              <div class="modal-body shadow-sm container text-secondary">
                <!-- add submit method here -->
                <form @submit.prevent="addVault">
                  <div class="row justify-content-center">
                    <div class="col text-center">
                      <!-- add v-model -->
                      <h5>Name:</h5>
                      <input type="text" placeholder="Name..." required v-model="newVault.name" />
                    </div>
                  </div>
                  <div class="row justify-content-center mt-3">
                    <div class="col text-center">
                      <!-- add v-model -->
                      <h5>Description:</h5>
                      <textarea
                        class="m-3"
                        rows="3"
                        type="text"
                        placeholder="Description..."
                        required
                        v-model="newVault.description"
                        style="width:90%;"
                      />
                    </div>
                  </div>
                  <div class="row justify-content-center mt-3">
                    <div class="col text-center">
                      <button type="submit" class="btn btn-secondary btn-lg">Add Vault</button>
                    </div>
                  </div>
                </form>
              </div>
              <div class="modal-footer bg-primary shadow-sm">
                <button type="button" class="btn btn-light" data-dismiss="modal">Cancel</button>
              </div>
            </div>
          </div>
        </div>
        <!-- END VAULT MODAL FORM -->

        <i
          class="fas fa-lock text-warning action"
          @click="toggleVaultList"
          v-show="!keep.isPrivate && $auth.isAuthenticated"
        >&nbsp;Add to Vault</i>
        <div id="vaultList" class="pl-3" v-show="showVaultList">
          <saveComponent
            v-for="vault in myVaults"
            :key="vault.id"
            :vault="vault"
            :keep="keep"
          >{{ vault.name }}</saveComponent>
        </div>
        <button
          type="button"
          class="btn btn-warning btn-sm shadow my-1 text-center ml-3"
          data-toggle="modal"
          data-target="#vaultModal"
          v-if="$auth.isAuthenticated && showVaultList"
        >Add Vault</button>
        <i
          class="fas fa-lock-open action"
          title="publish keep, button"
          v-show="keep.isPrivate == true"
          @click.stop.prevent="publish"
        >
          <small>&nbsp;Publish Keep</small>
        </i>
        <br />
        <i
          class="far fa-trash-alt text-danger action"
          v-show="keep.isPrivate == true"
          title="delete keep, button"
          @click.stop.prevent="deleteKeep"
        >
          <small>&nbsp;Delete Keep</small>
        </i>
        <br />
        <div v-show="keep.isPrivate">
          <i 
            class="fas fa-pencil-alt text-info action" 
            title="edit keep, button"
            @click="toggleEdit" >
            <small aria-hidden="true">&nbsp;Edit Keep</small>
          </i>
          <p />

          <!-- EDIT VAULT FORM -->
          <form class="form text-muted" @submit.prevent="editKeep" v-show="editDetails">
            Keep Name:
            <input type="text" v-model="keep.name" style="width:100%;" title="textbox" />
            <br />Keep Description:
            <input type="text" v-model="keep.description" style="width:100%;" title="textbox" />
            <br />

            <i
              type="submit"
              class="far fa-save text-info fa-2x action shadowtext-shadow"
              title="Save Changes"
              @click="updateKeep"
            ></i>
          </form>
          <!-- END EDIT VAULT FORM -->
        </div>
      </div>
    </div>
    <div class="col-12">
      <p>
        <small class="text-muted" v-show="keep.isPrivate">{{keep}}</small>
      </p>
    </div>
  </div>
</template>

<script>
import SaveComponent from "@/components/SaveComponent.vue";
import swal from "sweetalert";
export default {
  name: "keepdetails",
  data() {
    return {
      editDetails: false,
      showVaultList: false,
      newVault: {},
    };
  },
  async mounted() {
    await this.$store.dispatch("getKeepById", this.$route.params.keepId);
  },
  beforeRouteLeave(to, from, next) {
    if (to.name != "keepdetails") {
      this.$store.commit("setActiveKeep", {});
    }
    next();
  },
  computed: {
    keep() {
      return this.$store.state.activeKeep;
    },
    myVaults() {
      return this.$store.state.myVaults;
    },
  },
  methods: {
    updateKeep() {
      this.$store.dispatch("editKeep", this.keep);
      this.editDetails = false;
    },
    toggleEdit() {
      this.editDetails = !this.editDetails;
    },
    toggleVaultList() {
      this.showVaultList = !this.showVaultList;
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
    async addVault() {
      $("#vaultModal").modal("hide");
      await this.$store.dispatch("createVault", this.newVault);
      this.newVault = {};
      await this.$store.dispatch("getUserVaults");
    },
  },
  components: {
    SaveComponent,
  },
};
</script>

<style scoped></style>
