<script>
import Item from './Items.vue'

export default {
  name: 'App',
  components: {
    Item : Item
  },
  data (){
    return{
        num: 3
    }
  }
}
</script>

<template>
    <div class="column"
    v-for="n in num">
        <Item></Item>
    </div>
</template>

<style>

</style>