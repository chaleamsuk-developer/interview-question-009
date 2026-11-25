<script setup lang="ts">
import { onMounted, ref, nextTick } from 'vue'
import { createComment, getPost } from './services/api'
import type { PostItem } from './types'

const post = ref<PostItem | null>(null)
const message = ref('')
const loading = ref(true)
const sending = ref(false)
const error = ref('')
const commentInput = ref<HTMLTextAreaElement | null>(null)

function formatDate(value: string) {
  return new Intl.DateTimeFormat('en-GB', { day: '2-digit', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit' }).format(new Date(value))
}

async function loadPost() {
  loading.value = true
  error.value = ''
  try { post.value = await getPost(1) }
  catch { error.value = 'Unable to load the post. Please make sure the Backend API is running.' }
  finally { loading.value = false }
}

async function submitComment() {
  const text = message.value.trim()
  if (!text || sending.value) return
  sending.value = true
  error.value = ''
  try {
    const created = await createComment(1, text)
    if (post.value) post.value.comments.push(created)
    message.value = ''
    await nextTick()
    commentInput.value?.focus()
  } catch {
    error.value = 'Comment could not be saved. Please try again.'
  } finally { sending.value = false }
}

onMounted(loadPost)
</script>

<template>
  <main class="page-shell">
    <header class="topbar"><span>IT 08-1</span></header>
    <section class="feed" aria-label="Post and comments">
      <div v-if="loading" class="state-message">Loading post…</div>
      <div v-else-if="error && !post" class="state-message error-state">{{ error }} <button class="retry" @click="loadPost">Retry</button></div>
      <article v-else-if="post" class="post-card">
        <div class="post-heading">
          <div class="avatar avatar-blue">{{ post.authorName.charAt(0).toUpperCase() }}</div>
          <div class="author-meta"><strong>{{ post.authorName }}</strong><time>{{ formatDate(post.createdAtUtc) }}</time></div>
        </div>
        <h1 class="post-title">{{ post.title }}</h1>
        <img class="post-image" :src="post.imageUrl" alt="A puppy and a kitten sitting together in a grassy field" />

        <form class="comment-form" @submit.prevent="submitComment">
          <div class="avatar avatar-blue small-avatar">B</div>
          <div class="input-wrap">
            <textarea ref="commentInput" v-model="message" rows="1" maxlength="1000" placeholder="Comment" aria-label="Write a comment" @keydown.enter.exact.prevent="submitComment" @keydown.enter.shift.stop />
            <span class="input-hint">Press Enter to post · Shift + Enter for a new line</span>
          </div>
          <button class="send-button" type="submit" :disabled="!message.trim() || sending" :aria-label="sending ? 'Posting comment' : 'Post comment'">{{ sending ? '…' : '↵' }}</button>
        </form>
        <p v-if="error" class="inline-error" role="alert">{{ error }}</p>

        <div class="comments-list" aria-live="polite">
          <div v-for="comment in post.comments" :key="comment.id" class="comment-row">
            <div class="avatar avatar-blue small-avatar">{{ comment.authorName.charAt(0).toUpperCase() }}</div>
            <div class="comment-content">
              <div class="comment-bubble"><strong>{{ comment.authorName }}</strong><p>{{ comment.message }}</p></div>
              <time class="comment-time">{{ formatDate(comment.createdAtUtc) }}</time>
            </div>
          </div>
          <p v-if="post.comments.length === 0" class="empty-comments">Be the first to comment.</p>
        </div>
      </article>
    </section>
    <footer class="footer">Example.com · Interview Question 009</footer>
  </main>
</template>
