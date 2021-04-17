<template>
  <div class="text-center home mt-2" v-if="user != null">
    <p class="display-1 font-weight-normal">Dobrodošli, {{ user.username }}.</p>
  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: "Home",
  components: {},
  data() {
    return {
      user: null,
    };
  },
  created() {
    this.$token = localStorage.getItem("token");
    this.$logged = JSON.parse(localStorage.getItem("user"));
    if (
      localStorage.getItem("token") == null ||
      localStorage.getItem("user") == null
    ) {
      this.$router.push("/login");
    } else {
      this.user = JSON.parse(localStorage.getItem("user"));
    }
  },
};
</script>

<style scoped>
.theme--light.v-application {
  background: #ffffff;
}
.home {
  color: #4caf;
  background-color: white;
  max-width: 60%;
  margin: auto;
  border-radius: 10px;
}
</style>
