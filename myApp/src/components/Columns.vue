<script>
import Item from './Items.vue'

export default {
  name: 'App',
  components: {
    Item : Item
  },
  data() {
    return {
      posts: null,
    };
  },
  mounted () {
    axios
      .get('https://localhost:7026/Items')
      .then(response => (this.posts = response.data))
  }
}
</script>

<template>
    <div class="column"
    v-for="n in posts">
        <Item></Item>
    </div>
</template>

<style>

</style>