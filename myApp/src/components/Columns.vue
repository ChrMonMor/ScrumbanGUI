<script>
import Item from './Items.vue'

    export default {
        props: {
            ColumnsId: {
                type: Number,
                required: true
            },
        },
        name: 'App',
        components: {
            Item: Item
        },
        data() {
            return {
                posts: null
            };
        },
        computed: {
            startUrl() {
                return "https://localhost:7026/Columns/Items/" + this.ColumnsId;
            }
        },
        mounted() {
            axios.get(startUrl())
                .then(response => (this.posts = response.data))
        }
}
</script>

<template>
    <div class="Item"
    v-for="n in posts">
        <Item :ItemsId=n.Item_Id></Item>
    </div>
</template>

<style>

</style>