<template>
  <div class="dashboard container-fluid">
    <div class="row">
      <div class="col-12 text-center">
        <h1>DASHBOARD</h1>
      </div>

      <!-- MODAL BUTTONS -->
      <div class="col-12 d-flex justify-content-around">
        <button
          type="button"
          class="btn btn-warning btn-sm shadow my-1 text-center"
          data-toggle="modal"
          data-target="#vaultModal"
          v-if="$auth.isAuthenticated"
        >Add Vault</button>

        <button
          type="button"
          class="btn btn-primary btn-sm shadow my-1 text-center"
          data-toggle="modal"
          data-target="#keepModal"
          v-if="$auth.isAuthenticated"
        >Add Pic</button>
      </div>

      <!-- KEEP MODAL FORM -->
      <div class="modal fade" id="keepModal" role="dialog">
        <div class="modal-dialog">
          <!-- Modal content-->
          <div class="modal-content">
            <div class="modal-header bg-primary shadow-sm">
              <h4 class="modal-title text-white">New Pic</h4>
              <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body shadow-sm container text-secondary">
              <!-- add submit method here -->
              <form @submit.prevent="addKeep">
                <div class="row justify-content-center">
                  <div class="col text-center">
                    <!-- add v-model -->
                    <h5>Name:</h5>
                    <input type="text" placeholder="Name..." required v-model="newKeep.name" />
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
                      v-model="newKeep.description"
                      style="width:90%;"
                    />
                  </div>
                </div>
                <div class="row justify-content-center">
                  <div class="col text-center">
                    <h5>Image (optional):</h5>
                    <input type="text" placeholder="Image Link" required v-model="newKeep.img" />
                  </div>
                </div>

                <div class="row justify-content-center mt-3">
                  <div class="col text-center">
                    <button type="submit" class="btn btn-secondary btn-lg">Add Pic</button>
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
      <!-- END KEEP MODAL FORM -->

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
    </div>

    <div class="col-12 d-flex justify-content-around mb-3">
      <button class="btn btn-outline-warning" @click="toggleVaults">My Vaults</button>
      <button class="btn btn-outline-info" @click="toggleKeeps">My Pics</button>
    </div>

    <div class="col-12 list-container d-flex justify-content-center">
      <div id="keeps" class="card-columns p-2" v-show="showKeeps" style="column-gap: 1rem;">
        <!-- KEEP COMPONENTS BEGIN -->
        <keep v-for="keep in myKeeps" :key="keep.id" :keep="keep" />
        <!-- KEEP COMPONENTS END -->
      </div>
    </div>
    <div class="col-12 list-container d-flex justify-content-center">
      <div id="vaults" class="card-columns p-2" v-show="showVaults" style="column-gap: 1rem;">
        <!-- VAULT COMPONENTS BEGIN -->
        <vault v-for="vault in myVaults" :key="vault.id" :vault="vault" />
        <!-- VAULT COMPONENTS END -->
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "@/components/KeepComponent.vue";
import Vault from "@/components/VaultComponent.vue";
export default {
  name: "dashboard",
  data() {
    return {
      showKeeps: false,
      showVaults: false,
      newKeep: {},
      newVault: {},
    };
  },
  mounted() {
    this.$store.dispatch("getUserKeeps");
  },
  computed: {
    myKeeps() {
      return this.$store.state.myKeeps;
    },
    myVaults() {
      return this.$store.state.myVaults;
    },
  },
  methods: {
    toggleKeeps() {
      this.showKeeps = !this.showKeeps;
      if (this.showVaults == true) {
        this.showVaults = !this.showVaults;
      }
    },
    toggleVaults() {
      this.showVaults = !this.showVaults;
      if (this.showKeeps == true) {
        this.showKeeps = !this.showKeeps;
      }
    },
    async addKeep() {
      $("#keepModal").modal("hide");
      this.newKeep.isPrivate = true;
      this.newKeep.views = 0;
      this.newKeep.keeps = 0;
      await this.$store.dispatch("createKeep", this.newKeep);
      this.newKeep = {};
    },
    async addVault() {
      $("#vaultModal").modal("hide");
      await this.$store.dispatch("createVault", this.newVault);
      this.newVault = {};
      router.push({ name: "vaultdetails", params: { vaultId: res.data.id } });
    },
  },
  components: {
    Keep,
    Vault,
  },
};
</script>

<style></style>
